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

        [BindProperty]
        public Session session { get; set; }

        public IActionResult OnGet(int movieId)
        {
            session = new Session
            {
                MovieId = movieId,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2), 
                Movie = _context.Movies.Find(movieId) 
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (session.MovieId == 0)
            {
                ModelState.AddModelError("", "Movie is not specified.");
                return Page();
            }

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Movies/Schedule");
        }
    }
}
