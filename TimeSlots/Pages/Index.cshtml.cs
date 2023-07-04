using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeSlots.DataBase;
using TimeSlots.Model;

namespace TimeSlots.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly TimeslotsDbContext context;

		public IndexModel(ILogger<IndexModel> logger, TimeslotsDbContext context)
		{
			this.logger = logger;
			this.context = context;
		}

		public async Task OnGetAsync()
        {
            
        }
    }
}
