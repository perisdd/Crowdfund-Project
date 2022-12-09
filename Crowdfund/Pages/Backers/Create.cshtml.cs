using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Crowdfund.DB;
using Crowdfund.Models;

namespace Crowdfund.Pages.Backers
{
    public class CreateModel : PageModel
    {
        private FundDbContext Context { get; }

        [BindProperty]
        public Backer Backer { get; set; }

        public CreateModel(FundDbContext context)
        {
            Context = context;
        }


        public void OnGet()
        { }

        public async Task<ActionResult> OnPostAsync()
        {
            Context.Backers.Add(Backer);
            await Context.SaveChangesAsync();

            TempData["AlertMessage"] = "Backer Added Successfully!";
            return RedirectToPage("./Index");
        }
    }
}
