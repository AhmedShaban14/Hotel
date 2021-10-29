using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class HotelBrunch
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Hotel Brunch")]
        public string Name { get; set; }
        public string Location{ get; set; }
        public string Description { get; set; }
    }
}