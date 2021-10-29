using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;

using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace HotelBooking.Controllers
{
    public class RoomsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var rooms = db.Rooms.Include(x => x.RoomType).Include(x => x.HotelBrunch).Include(x => x.BookStatus).OrderBy(x => x.HotelBrunch.Name).ThenBy(x => x.RoomType.Name).ToList();
            return View(rooms);
        }
        [HttpGet]
        public ActionResult Index2()
        {
            var rooms = db.Rooms.Include(x => x.RoomType).Include(x => x.HotelBrunch).Include(x => x.BookStatus).ToList();
            return View(rooms);

        }
        [HttpGet]
        public ActionResult GetRoomsIds(string roomsIds)
        {
            var splitIds = roomsIds.Split(',');
            //int [] arr = new int[3];
            decimal sum = 0;
            int count = 0;
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
            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
            RoomsViewModel rvm = new RoomsViewModel
            {
                HotelBrunches = db.HotelBrunches.ToList(),
                RoomTypes = db.RoomTypes.ToList()
            };
            return View(rvm);
        }

        [HttpPost]
        public ActionResult New(RoomsViewModel rvm, HttpPostedFileBase ImageUrl)
        {
            if (!ModelState.IsValid)
            {
                rvm = new RoomsViewModel
                {
                    HotelBrunches = db.HotelBrunches.ToList(),
                    RoomTypes = db.RoomTypes.ToList()
                };
                return Json(new { result = 0, rvm });
            }
            //Save Image  :
            string defaultImage = @"Hotel.jpg";
            string uniqueFileName = "";
            if (ImageUrl != null)
            {
                var path = Server.MapPath("~/Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + ImageUrl.FileName;
                var fullPath = Path.Combine(path, uniqueFileName);
                ImageUrl.SaveAs(fullPath);
            }
            else
            {
                uniqueFileName = defaultImage;
            }
            var room = new Room()
            {
                RoomTypeId = rvm.RoomTypeId,
                HotelBrunchId = rvm.HotelBrunchId,
                Salary = rvm.Room.Salary,
                Description = rvm.Room.Description,
                Capacity = rvm.Room.Capacity,
                Image = uniqueFileName,
                BookStatusId = 1
            };
            db.Rooms.Add(room);
            db.SaveChanges();
            return Json(new { result = 1 });
        }
    }
}