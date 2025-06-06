using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data;
using Domain;

namespace Web.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly ILogger<CreateModel> _logger;
        private readonly AppDbContext _context;

        [BindProperty]
        public Movie movie { get; set; }

        public CreateModel(ILogger<CreateModel> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
          
            movie.Sessions = new List<Session>(); 
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            _logger.LogInformation("movie created successfully.");
            return RedirectToPage("/Movies/Schedule");
        }
    }
}
