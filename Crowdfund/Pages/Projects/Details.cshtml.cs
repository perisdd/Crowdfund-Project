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

		public void OnGet(int id)
        {
			Project = Context.Projects.SingleOrDefault(p => p.Id == id);
		}
    }
}
