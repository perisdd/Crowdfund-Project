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

        public List<Backer> Backers { get; set; }

        public IndexModel(FundDbContext context)
        {
            Context = context;
        }

        public async Task OnGet()
        {
            Backers = await Context.Backers.ToListAsync();
        }
    }
}
