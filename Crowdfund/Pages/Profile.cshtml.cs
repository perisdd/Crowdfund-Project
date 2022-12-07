using Crowdfund.DB;
using Crowdfund.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Pages
{
	public class ProfileModel : PageModel
	{
		private FundDbContext Context { get; }

		public Creator? Creator { get; set; }

		public Backer? Backer { get; set; }

		public ProfileModel(FundDbContext context)
		{
			Context = context;
		}

		private readonly int current = InitialModel.CurrentId;
		
		public void OnGet()
		{
			if (InitialModel.CurrentRole.Equals("Creator"))
				Creator = Context.Creators.Include(c => c.ProjectsCreated).SingleOrDefault(c => c.Id == current);
			else if (InitialModel.CurrentRole.Equals("Backer"))
				Backer = Context.Backers.Include(b => b.ProjectsInvested).Include(b => b.Contributions).SingleOrDefault(b => b.Id == current);
		}
	}
}
