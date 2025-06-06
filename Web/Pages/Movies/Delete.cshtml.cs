using Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Movies
{
    public class DeleteModel : PageModel
    {
        private readonly ILogger<CreateModel> _logger;
        private readonly AppDbContext _context;

        [BindProperty(SupportsGet = true)]
        public Movie movie { get; set; }
        public DeleteModel(ILogger<CreateModel> logger, AppDbContext context)
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
            var movieToDelete = _context.Movies.Find(movie.Id);

            if (movieToDelete == null)
            {
                _logger.LogWarning("Movie with ID {Id} not found.", movie.Id);
                return NotFound();
            }
            _context.Movies.Remove(movieToDelete);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Movie with ID {Id} deleted successfully.", movie.Id);
            return RedirectToPage("/Movies/Schedule");
        }
    }
}
