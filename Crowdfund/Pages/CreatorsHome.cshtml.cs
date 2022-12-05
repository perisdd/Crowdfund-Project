using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crowdfund.Pages
{
    public class CreatorsHomeModel : PageModel
    {
		public static int test { get; set; }

        public IActionResult OnPost(int creators)
        {
            test = creators;
            return RedirectToPage("Projects/Create");
        }

       public void OnGet()
        {

        }
    }
}
