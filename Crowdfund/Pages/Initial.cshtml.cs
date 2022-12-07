using Crowdfund.DB;
using Crowdfund.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crowdfund.Pages
{
	public class InitialModel : PageModel
	{
		private FundDbContext Context { get; }

		public List<Creator> Creators { get; set; }
		public List<Backer> Backers { get; set; }

		public static int CurrentId { get; set; }
		public static string CurrentRole { get; set; }

		public InitialModel(FundDbContext context)
		{
			Context = context;
		}

		public void OnGet()
		{
			Backers = Context.Backers.ToList();
			Creators = Context.Creators.ToList();
		}

		public IActionResult OnPost(int creator, int backer)
		{
			if (creator != 0)
			{
				CurrentId = creator;
				CurrentRole = "Creator";
			}

			if (backer != 0)
			{
				CurrentId = backer;
				CurrentRole = "Backer";
			}

			return RedirectToPage("Profile");
		}
	}
}
