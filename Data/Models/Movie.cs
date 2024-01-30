using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Data.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]      
        public string Title { get; set; }
        [Required]
        public int? Rate { get; set; }
        [Required]  
        public string Description { get; set; }
        public string ImageUrl { get; set; }  
    }
}
