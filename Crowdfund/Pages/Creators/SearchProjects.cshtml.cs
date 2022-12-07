using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crowdfund.DB;
using Crowdfund.Models;

namespace Crowdfund.Pages.Creators
{
    public class SearchProjectsModel : PageModel
    {
        private FundDbContext Context { get; }

        public List<string> Categories = Enum.GetNames(typeof(Category)).ToList();
        public Creator? Creator { get; set; }

        public List<Project> Projects { get; set; }


        /*		public IndexModel(ILogger<IndexModel> logger)
                {
                    _logger = logger;
                }
        */
        public SearchProjectsModel(FundDbContext context)
        {
            Context = context;
        }

/*
        public void OnGet(int id)
        {
            Backer = Context.Backers.SingleOrDefault(b => b.Id == id);
            Projects = Context.Projects.Where(p => p.Backers.Contains(Backer)).ToList();

             = Context.Contributions.Where(c => c.Backer.Id == id).ToList();
        }
*/
        public async Task OnGetAsync(string searchbar)
        {
            //            Creator = Context.Creators.SingleOrDefault(b => b.Id == id);
            //            Projects = Context.Projects.Where(p => p.Creators.Contains(Creators)).ToList();
            ////            var proje = from pro in Context.Projects select pro;

            var proj = from pro in Context.Projects select pro;

            if (!String.IsNullOrEmpty(searchbar))
            {
                proj = proj.Where(proje => proje.Title.ToLower().Contains(searchbar.ToLower()) ||
                proje.Description.ToLower().Contains(searchbar.ToLower()));
            }

            Projects = await proj.AsNoTracking().ToListAsync();
        }


        public async Task OnPostSearchByCategory(Category cat)
        {
            var proje = from pro in Context.Projects select pro;

            if (cat.ToString() != null)
            {
                proje = proje.Where(proj => proj.ProjectCategory.Equals(cat));
            }

            Projects = await proje.AsNoTracking().ToListAsync();
        }

    }
     
}

