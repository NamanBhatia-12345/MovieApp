using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieApp.Data;
using MovieApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Pages
{
    public class EditMovieModel : PageModel
    {
        [BindProperty]
        public IFormFile Photo { get; set; }    
        public Movie Movie { get; set; }

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public EditMovieModel(ApplicationDbContext context,
            IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }
        public void OnGet(int id)
        {
            ViewData["id"] = id;
            Movie = _context.Movies.FirstOrDefault(x => x.Id == id);
        }
        public async Task<IActionResult> OnPost(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Movie updatedObj = new Movie
            {
                Id = movie.Id,
                Title = movie.Title,
                Rate = movie.Rate,
                Description = movie.Description
            };
            if(Photo != null) 
            {
                if(movie.ImageUrl != null) 
                {
                    string filePath = Path.Combine(_webHost.WebRootPath, "images", movie.ImageUrl);
                    System.IO.File.Delete(filePath);    
                }
                updatedObj.ImageUrl = await ProcessUploadedFile();
            }   
            _context.Movies.Update(updatedObj);     
            await _context.SaveChangesAsync();      
            return RedirectToPage("Index");
        }

        private async Task<string> ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if(Photo != null) 
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                Console.WriteLine($"The uploaded folder is : {uploadsFolder}");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                Console.WriteLine($"The filepath is : {filePath}");
                using (var fileStream = new FileStream(filePath,FileMode.Create))
                {
                    await Photo.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
