using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Data.Models;

namespace MovieApp.Pages
{
    public class MovieModel : PageModel
    {
        public IFormFile Photo { get; set; }        
        public Movie Movie { get; set; }        
        private readonly ApplicationDbContext _context;
        public MovieModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet(int id)
        {
            ViewData["id"] = id;        
            Movie = _context.Movies.FirstOrDefault(x => x.Id == id);
        }
    }
}
