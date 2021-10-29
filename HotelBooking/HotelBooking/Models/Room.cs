using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Add Description Of Room .")]
        [Display(Name ="Room Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Add Capacity Of Room .")]
        [Display(Name = "Room Capacity")]
        public int Capacity { get; set; }
        [Required(ErrorMessage = "Please Add Salary Of Room .")]
        [Display(Name = "Room Salary")]
        public decimal Salary { get; set; }
        [Display(Name = "Room Image")]
        public string Image{ get; set; }
        [ForeignKey("RoomTypeId")]
        public RoomType  RoomType{ get; set; }
        [Display(Name = "Room Type")]
        public int RoomTypeId{ get; set; }
        [ForeignKey("HotelBrunchId")]
        public HotelBrunch HotelBrunch { get; set; }
        [Display(Name = "Hotel Brunch")]
        public int HotelBrunchId { get; set; }
        [ForeignKey("BookStatusId")]
        public BookStatus BookStatus { get; set; }
        public int BookStatusId { get; set; }
    }
}