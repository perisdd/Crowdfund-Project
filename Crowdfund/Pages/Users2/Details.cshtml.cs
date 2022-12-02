using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Crowdfund.DB;
using Microsoft.EntityFrameworkCore;
using Crowdfund.Models;

namespace Tsontarw_Razor.Pages.Users
{
    public class DetailsModel : PageModel
    {
       
            private FundDbContext Context { get; }
            public User? User { get; set; }
            public List<Project> UserProjects { get; set; }

            public DetailsModel(FundDbContext context)
            {
                Context = context;
            }

            public async Task<ActionResult> OnGet(int id)
            {
                User = await Context.Backers.SingleOrDefaultAsync(a => a.Id == id);
                if (User is null) return BadRequest();
                UserProjects = await Context.Projects.Where(m => m.Backers.Contains(User)).ToListAsync();
                return Page();
            }

        
    }
}
