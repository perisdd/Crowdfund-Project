using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Crowdfund.DB;
using Microsoft.EntityFrameworkCore;
using Crowdfund.Models;

namespace Crowdfund.Pages.Creators
{
    public class DetailsModel : PageModel
    {
        private FundDbContext Context { get; }

        public Creator Creator { get; set; }

        public List<Project> Projects { get; set; }

        public DetailsModel(FundDbContext context)
        {
            Context = context;
        }

        public async Task<ActionResult> OnGet(int id)
        {
            Creator = await Context.Creators.SingleOrDefaultAsync(c => c.Id == id);
            if (Creator == null) { return BadRequest(); }

            Projects = await Context.Projects.Where(p => p.Creator.Id == id).ToListAsync();
            return Page();
        }
    }
}
