using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeSlots.DataBase;
using TimeSlots.Model;

namespace TimeSlots.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TimeslotsDbContext _context;
        public IEnumerable<Platform>? Platforms { get; set; }
        public IEnumerable<GateSchedule>? GatesSchedules { get; set; }
        public IEnumerable<Gate>? Gates { get; set; } 
        public IEnumerable<Company>? Companies { get; set; }
        public IEnumerable<Timeslot>? Timeslots { get; set; }
        public IEnumerable<PlatformFavorite>? PlatformFavorites { get; set; }

		public IndexModel(TimeslotsDbContext context)
		{
			_context = context;
		}

		public async Task OnGetAsync()
        {
            await Refresh();
        }

        public async Task<IActionResult> OnPostClearTimeslots()
        {
            if (await _context.Timeslots.CountAsync() > 0)
            {
                await _context.Timeslots?.ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
                await Refresh();
            }

            return RedirectToPage();
        }

        private async Task Refresh()
        {
			Platforms = await _context.Platforms
				.Include(x => x.Companies)
				.Include(x => x.Gates)
				.ToListAsync();
			GatesSchedules = await _context.GateSchedules
				.ToListAsync();
			Gates = await _context.Gates
				.Include(x => x.Timeslots)
				.Include(x => x.GateSchedules)
				.ToListAsync();
			Companies = await _context.Companies
				.Include(x => x.GateSchedule)
				.ToListAsync();
			Timeslots = await _context.Timeslots
				.ToListAsync();
            PlatformFavorites = await _context.PlatformFavorites
                .ToListAsync();
		}
    }
}
