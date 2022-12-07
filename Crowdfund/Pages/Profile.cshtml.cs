using Crowdfund.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crowdfund.Pages
{
    public class ProfileModel : PageModel
    {
		private FundDbContext Context { get; }

		public ProfileModel(FundDbContext context)
		{
			Context = context;
		}

		public void OnGet()
        {

        }
    }
}
