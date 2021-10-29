using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class BookingDetails
    {
        [Key]
        public int Id { get; set; }
        public int BookingHeaderId { get; set; }
        public int RoomId { get; set; }

        [ForeignKey("BookingHeaderId")]
        public BookingHeader BookingHeader { get; set; }

        [ForeignKey("RoomId")]
        public Room Room{ get; set; }
    }
}