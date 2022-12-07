using Crowdfund.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Crowdfund.DB
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public List<string> Categories = Enum.GetNames(typeof(Category)).ToList();
        private FundDbContext Context { get; }

        public Creator? Creator { get; set; }

        public List<Project> Projects { get; set; }


        /*		public IndexModel(ILogger<IndexModel> logger)
                {
                    _logger = logger;
                }
        */
        public IndexModel(FundDbContext context)
        {
            Context = context;
        }

        public async Task OnGetAsync(string searchbar)
        {

            var proj = from pro in Context.Projects select pro;

            if (!String.IsNullOrEmpty(searchbar))
            {
                proj = proj.Where(proje => proje.Title.ToLower().Contains(searchbar.ToLower()) ||
                proje.Description.ToLower().Contains(searchbar.ToLower()));
            }

            Projects = await proj.AsNoTracking().ToListAsync();
        }


    }
}