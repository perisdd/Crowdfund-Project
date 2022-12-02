using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Crowdfund.Models;
using Crowdfund.DB;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Pages.Users
{
    public class CreateModel : PageModel
    {
        private FundDbContext Context { get; }
        public int UserCount { get; set; }
        [BindProperty] public Backer Backer { get; set; }

        public CreateModel(FundDbContext context)
        {
            Context = context;
        }

        public void OnGet()
        {
            UserCount = Context.Backers.Count();
        }

        public async Task<ActionResult> OnPost()
        {
            Context.Backers.Add(Backer);
            await Context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
 }   
