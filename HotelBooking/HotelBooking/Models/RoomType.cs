using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class RoomType
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Room Type ")]
        public string Name { get; set; }
    }
}