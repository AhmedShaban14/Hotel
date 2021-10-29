
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class RoomsViewModel
    {

        public Room Room { get; set; }
        //public string Description { get; set; }
        
        [Required(ErrorMessage ="Please Choose Room Type ")]
        public int RoomTypeId { get; set; }
        [Required(ErrorMessage = "Please Choose Hotel Brunch ")]
        public int HotelBrunchId { get; set; }
        [ForeignKey("RoomTypeId")]
        public IEnumerable<RoomType> RoomTypes { get; set; }
        [ForeignKey("HotelBrunchId")]
        public IEnumerable<HotelBrunch> HotelBrunches { get; set; }
    }
}