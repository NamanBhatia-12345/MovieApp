using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieApp.Data;
using MovieApp.Data.Models;

namespace MovieApp.Pages
{
    public class AddMovieModel : PageModel
    {
        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public Movie Movie { get; set; }    
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        public AddMovieModel(ApplicationDbContext context, 
                            IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        public void OnGet() { }
        public async Task<IActionResult> OnPost() 
        {
            string value = $"{Movie.Title} - {Movie.Rate} - {Movie.Description}";
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Movie obj = new Movie
            {
                Title = Movie.Title,
                Rate = Movie.Rate,
                Description = Movie.Description,
            };
            obj.ImageUrl = await ProcessUploadedFile();
            await _context.Movies.AddAsync(obj);
            await _context.SaveChangesAsync();
            return Redirect("Index");
        }
        private async Task<string> ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                Console.WriteLine($"The uploaded folder is : {uploadsFolder}");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                Console.WriteLine($"The filepath is : {filePath}");
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Photo.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
