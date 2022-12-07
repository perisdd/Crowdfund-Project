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

		public ProfileModel(FundDbContext context)
		{
			Context = context;
		}

		private readonly int current = InitialModel.test;
		
		public void OnGet()
		{
			Creator = Context.Creators.Include(c => c.ProjectsCreated).SingleOrDefault(c => c.Id == current);
		}
	}
}
