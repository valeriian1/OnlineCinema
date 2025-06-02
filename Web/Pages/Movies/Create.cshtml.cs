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

        [BindProperty(SupportsGet = true)]
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

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Movie created successfully.");
            return RedirectToPage("/Sessions/Create");
        }
    }
}
