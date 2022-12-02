using Crowdfund.DB;
using Crowdfund.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Pages.Backers
{
    public class IndexModel : PageModel
    {
        private FundDbContext Context { get; }
        public List<Project> Projects { get; set; }

        public IndexModel(FundDbContext context)
        {
            Context = context;
        }

        public async Task OnGet()
        {
            Projects = await Context.Projects.ToListAsync();
        }
    }
}
