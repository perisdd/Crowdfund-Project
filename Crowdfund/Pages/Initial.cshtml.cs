using Crowdfund.DB;
using Crowdfund.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crowdfund.Pages
{
    public class InitialModel : PageModel
    {
		public static int test { get; set; }
		public static string test2 { get; set; }

		private FundDbContext Context { get; }

		public List<Creator> Creators { get; set; }
		public List<Backer> Backers { get; set; }

		public InitialModel(FundDbContext context)
		{
			Context = context;
		}

		public IActionResult OnPost(int creators, int backers)
		{
			if (creators != 0)
			{
				test = creators;
				test2 = "Creator";
			}
			else if (backers != 0)
			{
				test = backers;
				test2 = "Backer";
			}
			else
			{
				test = 0;
				test2 = "N/A";
			}

            return RedirectToPage("Profile");
        }
		public void OnGet()
		{
			Backers = Context.Backers.ToList();
			Creators = Context.Creators.ToList();

		}
    }
}
