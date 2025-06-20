using Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Web.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;
        private readonly AppDbContext _context;

        [BindProperty(SupportsGet = true)]
        public Movie movie { get; set; }

        public EditModel(ILogger<EditModel> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingMovie = await _context.Movies
                .Include(m => m.Sessions)
                .FirstOrDefaultAsync(m => m.Id == movie.Id);

            if (existingMovie == null)
            {
                _logger.LogWarning("movie with ID {Id} not found.", movie.Id);
                return NotFound();
            }

            existingMovie.Title = movie.Title;
            existingMovie.Description = movie.Description;
            existingMovie.ReleaseDate = movie.ReleaseDate;
            existingMovie.Director = movie.Director;
            existingMovie.MovieCast = movie.MovieCast;
            existingMovie.Genre = movie.Genre;
            existingMovie.PosterUrl = movie.PosterUrl;

            await _context.SaveChangesAsync();
            _logger.LogInformation("movie with ID {Id} updated successfully.", movie.Id);
            TempData["SuccessMessage"] = "Edited successfully!";
            return RedirectToPage("/Movies/Schedule");
        }
    }
}
