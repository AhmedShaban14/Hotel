using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.ViewModels
{
    public class BookingViewModel
    {
        public BookingHeader BookingHeader { get; set; }
        public IEnumerable<BookingDetails> BookingDetails { get; set; }
    }
}