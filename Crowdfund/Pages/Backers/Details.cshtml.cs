using Crowdfund.DB;
using Crowdfund.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crowdfund.Pages.Backers
{
    public class DetailsModel : PageModel
    {
        private FundDbContext Context { get; }

        public Backer Backer { get; set; }

        public List<Project> Projects { get; set; }

        public List<Contribution> Contributions { get; set; }

        public DetailsModel(FundDbContext context)
        {
            Context = context;
        }

        public void OnGet(int id)
        {
            Backer = Context.Backers.SingleOrDefault(b => b.Id == id);
            Projects = Context.Projects.Where(p => p.Backers.Contains(Backer)).ToList();

            Contributions = Context.Contributions.Where(c => c.Backer.Id == id).ToList();
        }
    }
}
