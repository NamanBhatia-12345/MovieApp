using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Data.Models;

namespace MovieApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }  
        public IEnumerable<Movie> Movies { get; set; }  
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; 
        }
        public void OnGet(string searchTerm = null)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrEmpty(searchTerm))
            {
                Movies = _context.Movies.ToList();
            }
            else
            {
                Movies = _context.Movies.Where(e => e.Title.Contains(searchTerm)
                                            || e.Description.Contains(searchTerm)).ToList();
            }
        }
    }
}
