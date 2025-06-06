using Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Web.Pages.Movies
{
    public class ScheduleModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ScheduleModel> _logger;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>();

        public ScheduleModel(AppDbContext context, ILogger<ScheduleModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            IQueryable<Movie> query = _context.Movies.Include(m => m.Sessions);

            if (!String.IsNullOrEmpty(SearchString))
            {
                string lowerSearchString = SearchString.ToLowerInvariant();
                query = query.Where(m =>
                    (m.Title != null && m.Title.ToLowerInvariant().Contains(lowerSearchString)) ||
                    (m.Director != null && m.Director.ToLowerInvariant().Contains(lowerSearchString)) ||
                    (m.Genre != null && m.Genre.ToLowerInvariant().Contains(lowerSearchString)) ||
                    (m.Description != null && m.Description.ToLowerInvariant().Contains(lowerSearchString)) ||
                    (m.Sessions != null && m.Sessions.Any(s =>
                        s.StartTime.ToString("HH:mm").Contains(lowerSearchString) ||
                        s.StartTime.ToString("dd.MM").Contains(lowerSearchString) ||
                        s.StartTime.ToString("dd.MM.yyyy").Contains(lowerSearchString)
                    ))
                );
            }

            Movies = await query.OrderBy(m => m.Title).ToListAsync();
        }
    }
}
