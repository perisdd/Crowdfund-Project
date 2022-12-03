using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Crowdfund.Models;
using Crowdfund.DB;

namespace Crowdfund.Pages.Creators
{
    public class CreateModel : PageModel
    {
        private FundDbContext Context { get; }

        [BindProperty] public Creator Creator { get; set; }

        public CreateModel(FundDbContext context)
        {
            Context = context;
        }

        public void OnGet()
        { }

        public async Task<ActionResult> OnPost()
        {
            Context.Creators.Add(Creator);
            await Context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
