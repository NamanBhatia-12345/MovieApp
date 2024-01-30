using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Data.Models;

namespace MovieApp.Pages
{
    public class DeleteMovieModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        public DeleteMovieModel(ApplicationDbContext context,
                                IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        [BindProperty]
        public Movie Movie { get; set; }
        public void OnGet(int id)
        {
            ViewData["id"] = id;
            Movie = _context.Movies.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == Movie.Id);        
            if(movie.ImageUrl != null)
            {
                string filePath = Path.Combine(_webHost.WebRootPath, "images", movie.ImageUrl);
                System.IO.File.Delete(filePath);
            }
            _context.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
