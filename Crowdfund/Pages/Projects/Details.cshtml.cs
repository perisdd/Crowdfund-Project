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

		public Contribution Contribution { get; set; }

		public List<Backer> Backers { get; set; }

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

			Backers = Context.Backers.
				Include(b => b.ProjectsInvested).
				Include(b => b.Contributions).
				ToList();
		}

        public async Task<IActionResult> OnPost()
        {
            int id = InitialModel.CurrentId;
            var Back = await Context.Backers.SingleOrDefaultAsync(c => c.Id == id);

            if (Back == null) return NotFound("Invalid backer Id");

            var proje = await Context.Projects.SingleOrDefaultAsync(p => p)
            
			Context.Contributions.Add(contribution);

            Context.Projects.Add(Project);
            await Context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
