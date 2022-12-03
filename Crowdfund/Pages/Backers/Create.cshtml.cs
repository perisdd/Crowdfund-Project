using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Crowdfund.DB;
using Crowdfund.Models;

namespace Crowdfund.Pages.Backers
{
    public class CreateModel : PageModel
    {
        private FundDbContext Context { get; }
 
        public int BackerCount { get; set; }

        [BindProperty]
        public Backer Backer { get; set; }

        
        public CreateModel(FundDbContext context)
        {
            Context = context;
        }


        public void OnGet()
        {
            BackerCount = Context.Backers.Count();
        }


        public async Task<ActionResult> OnPostAsync()
        {
            Context.Backers.Add(Backer);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
