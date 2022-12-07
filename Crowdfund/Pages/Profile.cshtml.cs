using Crowdfund.DB;
using Crowdfund.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Pages
{
	public class ProfileModel : PageModel
	{
		public Backer? Backer { get; set; }

		public Creator? Creator { get; set; }

		private FundDbContext Context { get; }

		public ProfileModel(FundDbContext context)
		{
			Context = context;
		}
		
		public void OnGet()
		{
			if (InitialModel.CurrentRole.Equals("Creator"))
			{
				Creator = Context.Creators
					.Include(c => c.ProjectsCreated)
					.SingleOrDefault(c => c.Id == InitialModel.CurrentId);
			}

			else if (InitialModel.CurrentRole.Equals("Backer"))
			{
				Backer = Context.Backers
					.Include(b => b.ProjectsInvested)
					.Include(b => b.Contributions)
					.SingleOrDefault(b => b.Id == InitialModel.CurrentId);
			}
		}
	}
}
