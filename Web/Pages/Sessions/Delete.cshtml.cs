using Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Sessions
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DeleteModel> _logger;

        [BindProperty(SupportsGet = true)]
        public Session session { get; set; }
        public DeleteModel(ILogger<DeleteModel> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            session = await _context.Sessions.FindAsync(id);

            if (session == null)
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
            var existingSession = _context.Sessions.Find(session.Id);
            if (existingSession == null)
            {
                _logger.LogWarning("Session {id} not found.", session.Id);
                return NotFound();
            }

            _context.Sessions.Remove(existingSession);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Session {id} deleted successfully.", session.Id);
            return RedirectToPage("/Movies/Schedule");
        }
    }
}
