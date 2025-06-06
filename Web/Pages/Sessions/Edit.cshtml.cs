using Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Sessions
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EditModel> _logger;
        public EditModel(ILogger<EditModel> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Session session { get; set; }
        public void OnGet()
        {
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
            existingSession.StartTime = session.StartTime;
            existingSession.EndTime = session.EndTime;

            _context.Sessions.Update(existingSession);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Session {id} updated successfully.", session.Id);
            return RedirectToPage("/Movies/Schedule");
        }

    }
}
