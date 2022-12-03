using Crowdfund.DB;
using Crowdfund.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crowdfund.Pages.Creators
{
    public class IndexModel : PageModel
    {
        private FundDbContext Context { get; }

        public List<Creator> Creators { get; set; }

        public IndexModel(FundDbContext context)
        {
            Context = context;
        }

        public void OnGet()
        {
            Creators = Context.Creators.ToList();
        }
    }
}
