using Crowdfund.DB;
using Crowdfund.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crowdfund.Pages
{
    public class InitialModel : PageModel
    {
		public static int test { get; set; }

		private FundDbContext Context { get; }

		public List<Creator> Creators { get; set; }
		public List<Backer> Backers { get; set; }

		public InitialModel(FundDbContext context)
		{
			Context = context;
		}

		public IActionResult OnPost(int creators)
        {
            test = creators;
            return RedirectToPage("Projects/Create");
        }
		public void OnGet()
		{
			Backers = Context.Backers.ToList();
			Creators = Context.Creators.ToList();

		}
    }
}
