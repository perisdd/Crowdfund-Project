using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Crowdfund.Models;
using Crowdfund.DB;

namespace Crowdfund.Pages.Projects
{
    public class DetailsModel : PageModel
    {
		private FundDbContext Context { get; }

		public Project Project { get; set; }

		public DetailsModel(FundDbContext context)
		{
			Context = context;
		}

		public void OnGet(int id)
        {
			Project = Context.Projects.
				Include(p => p.Rewards).
				Include(p => p.Backers).
				Include(p => p.Creator).
				SingleOrDefault(p => p.Id == id);
		}
    }
}
