
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class BookingHeader
    {
        [Key]
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser User{ get; set; }
        public decimal TotalPrice { get; set; }
        public decimal NetPrice { get; set; }
        public int RoomsCount { get; set; }

    }
}