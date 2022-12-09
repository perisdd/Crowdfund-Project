using Crowdfund.DB;
using Crowdfund.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Pages.Projects
{
    public class IndexModel : PageModel
    {
		private FundDbContext Context { get; }
		
		public List<Project> Projects { get; set; }

		public string CurrentFilter { get; set; }

		public IndexModel(FundDbContext context)
		{
			Context = context;
		}

        public async Task OnGetAsync(string search)
        {
			CurrentFilter = search;

			IQueryable<Project> projectsIQ = from p in Context.Projects select p;

			if (!String.IsNullOrEmpty(search))
			{
				projectsIQ = projectsIQ.Where(p =>
					p.Title.ToLower().Contains(search.ToLower()) ||
					p.Description.ToLower().Contains(search.ToLower())
				);
			}

			Projects = await projectsIQ.AsNoTracking().ToListAsync();
		}
    }
}
