using Crowdfund.DB;
using Crowdfund.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crowdfund.Pages.Projects
{
    public class IndexModel : PageModel
    {
		private FundDbContext Context { get; }
		
		public List<Project> Projects { get; set; }

		public IndexModel(FundDbContext context)
		{
			Context = context;
		}

        public void OnGet()
        {
			Projects = Context.Projects.ToList();
        }
    }
}
