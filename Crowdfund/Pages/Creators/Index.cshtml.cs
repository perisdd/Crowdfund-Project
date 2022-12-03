using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Crowdfund.DB;
using Crowdfund.Models;
using Microsoft.EntityFrameworkCore;


namespace Crowdfund.Pages.Creators
{
    public class CreatorsModel : PageModel
    {
        private FundDbContext Context { get; }
        public List<Creator> Creators { get; set; } = null!;

        public CreatorsModel(FundDbContext context)
        {
            Context = context;
        }

        public async Task OnGet()
        {
            Creators = await Context.Creators.ToListAsync();
        }
    }
}
