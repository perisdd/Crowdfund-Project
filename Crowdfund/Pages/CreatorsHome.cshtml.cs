using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crowdfund.Pages
{
    public class CreatorsHomeModel : PageModel
    {

        public IActionResult OnPost(int creators)
        {
            TempData["creator"] = creators;
            return RedirectToPage("Projects/Create");
        }

       public void OnGet()
        {

        }
    }
}
