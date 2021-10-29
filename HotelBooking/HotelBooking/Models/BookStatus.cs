using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class BookStatus
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Room Status ")]
        public string Name { get; set; }
    }
}