using Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace Web.Pages.Movies
{
    public class ScheduleModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ScheduleModel> _logger;

        [BindProperty(SupportsGet = true)]
        public int? SelectedId { get; set; }

        public List<Movie> Movies { get; set; }

        public ScheduleModel(AppDbContext context, ILogger<ScheduleModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            try
            {
                Movies = _context.Movies
                    .Include(m => m.Sessions)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the movie schedule.");
            }
        }
    }
}
