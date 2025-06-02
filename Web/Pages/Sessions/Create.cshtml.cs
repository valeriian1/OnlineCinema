using Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Web.Pages.Sessions
{
    public class CreateModel : PageModel
    {
        private readonly ILogger<CreateModel> _logger;
        private readonly AppDbContext _context;
        public CreateModel(ILogger<CreateModel> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public Session Session { get; set; }

        public async Task<IActionResult> OnGetAsync(int movieId)
        {
            Session = new Session { MovieId = movieId };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Sessions.Add(Session);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Movies/Schedule", new { id = Session.MovieId });
        }
    }
}
