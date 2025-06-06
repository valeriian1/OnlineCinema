using Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Movies
{
    public class DeleteModel : PageModel
    {
        private readonly ILogger<DeleteModel> _logger;
        private readonly AppDbContext _context;

        public DeleteModel(ILogger<DeleteModel> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public Movie movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (movie == null || movie.Id == 0)
            {
                return BadRequest();
            }

            var movieToDelete = await _context.Movies.FindAsync(movie.Id);

            if (movieToDelete == null)
            {
                _logger.LogWarning("movie with ID {Id} not found.", movie.Id);
                return NotFound();
            }

            _context.Movies.Remove(movieToDelete);
            await _context.SaveChangesAsync();

            _logger.LogInformation("movie with ID {Id} deleted successfully.", movie.Id);
            return RedirectToPage("/Movies/Schedule");
        }
    }
}
