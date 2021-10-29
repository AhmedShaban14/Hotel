using HotelBooking.Models;
using HotelBooking.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBooking.Controllers
{
    public class BookingController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        public ActionResult Index()
        {
            BookingViewModel bvm = new BookingViewModel
            {
                BookingHeader = new BookingHeader(),
                BookingDetails = new List<BookingDetails>()
            };
            var rooms = db.Rooms.Where(x => x.BookStatusId == 1).Include(x => x.RoomType).Include(x => x.HotelBrunch).Include(x => x.BookStatus).OrderBy(x => x.HotelBrunch.Name).ThenBy(x => x.RoomType.Name).ToList();
            ViewBag.roomList = rooms;
            return View(bvm);
        }
        [HttpPost]
        public ActionResult Index(BookingViewModel bvm, string roomsIds)
        {
            bvm = new BookingViewModel { BookingHeader = new BookingHeader(), BookingDetails = new List<BookingDetails>() };
            decimal total = 0;
            int count = 0;
//Calculate Room's Ids : 
            GetRoomsIds(roomsIds, out total, out count);
            List<Room> list = TempData["roomList"] as List<Room>;
            if (Session["userId"] != null)
            {
                var userIdSession = UserIdSession();
//Add Info To Booking Header: 
                AddInfoToBookingHeader(bvm.BookingHeader, userIdSession, total, count);
//Calculate Descount : 
                var userBooking = db.BookingHeaders.FirstOrDefault(x => x.UserID == userIdSession);
                if (userBooking != null)
                {
                    if (!CalculateDiscount(bvm.BookingHeader, UserInfo(userIdSession)))
                        return Json(new { result = 0 });
                }
                else
                {
                    if (!CalculateWithOutDiscount(bvm.BookingHeader, UserInfo(userIdSession)))
                        return Json(new { result = 2 });
                }
                db.BookingHeaders.Add(bvm.BookingHeader);
                db.SaveChanges();
//Make Room Availabe Or Not : 
                MakeRoomAvailableOrNot(list);
//Save Order Details : 
                if (SaveOrderDetails(list,bvm.BookingHeader))
                    return Json(new { result = 1 });
            }
            return Json(new { result = 0 });
        }
        [HttpPost]
        public ActionResult SearchList(string text)
        {
            var rooms = db.Rooms.Where(x => x.RoomType.Name.Contains(text) || x.HotelBrunch.Name.Contains(text)).Include(x => x.RoomType).Include(x => x.HotelBrunch).Include(x => x.BookStatus).OrderBy(x => x.HotelBrunch.Name).ThenBy(x => x.RoomType.Name).ToList();
            ViewBag.roomList = rooms;
            return PartialView("_SearchList");
        }
        [HttpGet]
        public ActionResult GetRoomsIds(string roomsIds, out decimal sum, out int count)
        {
            var splitIds = roomsIds.Split(',');
            sum = 0;
            count = 0;
            int[] myInts = Array.ConvertAll(splitIds, int.Parse);
            var roomList = new List<Room>();
            foreach (var roomId in myInts)
            {
                var room = db.Rooms.Include(x => x.RoomType).Include(x => x.HotelBrunch).Include(x => x.BookStatus)
                    .SingleOrDefault(x => x.Id == roomId);
                count++;
                sum += room.Salary;
                roomList.Add(room);
            }
            TempData["roomList"] = roomList;

            return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
        }
        public void MakeRoomAvailableOrNot(List<Room> roomList)
        {
            foreach (var r in roomList)
            {
                var room = db.Rooms.SingleOrDefault(x => x.Id == r.Id);
                if (room.RoomTypeId == 1)
                {
                    //Single 
                    //Make it Un Availabe : 
                    room.BookStatusId = 2;
                    db.SaveChanges();
                }
                else if (room.RoomTypeId == 2 || room.RoomTypeId == 3)
                {
                    //Double Or Suite 
                    //Make it Available : 
                    room.BookStatusId = 1;
                    db.SaveChanges();
                }
            }
        }
        public bool SaveOrderDetails(List<Room> roomList,BookingHeader bookingHeader)
        {
            if(roomList ==null || bookingHeader == null)
            {
                return false;
            }
            foreach (var room in roomList)
            {
                var orderDetail = new BookingDetails()
                {
                    BookingHeaderId = bookingHeader.Id,
                    RoomId = room.Id,
                    Room = room
                };
                db.BookingDetails.Add(orderDetail);
                db.SaveChanges();
            }
            return true;
        }
        public bool CalculateDiscount(BookingHeader bookingHeader, ApplicationUser user)
        {
            //Calculate Descount Here: 
            //Total Salary=> 1000
            //TempDescount=> 950
            //Net=> Tootal -Temp 
            var tempDescount = (bookingHeader.TotalPrice * 95 / 100);
            bookingHeader.NetPrice = bookingHeader.TotalPrice - tempDescount;
            //Update User Wallet : 
            if (user.Wallet > bookingHeader.NetPrice)
            {
                user.Wallet = user.Wallet - bookingHeader.NetPrice;
                return true;
            }
            return false;

        }
        public bool CalculateWithOutDiscount(BookingHeader bookingHeader, ApplicationUser user)
        {
            //User First Time To BOOK :
            bookingHeader.NetPrice = 0;
            if (user.Wallet > bookingHeader.TotalPrice)
            {
                user.Wallet = user.Wallet - bookingHeader.TotalPrice;
                return true;
            }
            return false;
        }
        public void AddInfoToBookingHeader(BookingHeader bookingHeader,string userIdSession,decimal total,int count)
        {
            bookingHeader.UserID = userIdSession;
            bookingHeader.BookingDate = DateTime.Now;
            bookingHeader.TotalPrice = total;
            bookingHeader.RoomsCount = count;
        }
        public string UserIdSession()
        {
            return Session["userId"].ToString();
        }
        public ApplicationUser UserInfo(string userIdSession)
        {
            return db.Users.SingleOrDefault(x => x.Id == userIdSession);
        }
    }
}