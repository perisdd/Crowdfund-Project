using Crowdfund.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crowdfund.DB;
using System.Linq;
using System.Diagnostics.Metrics;

namespace Crowdfund.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public List<string> Categories = Enum.GetNames(typeof(Category)).ToList();
        private FundDbContext Context { get; }

        public Creator? Creator { get; set; }

        public List<Project> Projects { get; set; }

		//public IndexModel(ILogger<IndexModel> logger)
		//{
		//	_logger = logger;
		//}
        private FundDbContext Context { get; }

        public Creator? Creator { get; set; }

        public Backer? Backer { get; set; }
        public IndexModel(FundDbContext context)
        {
            Context = context;
        }
        private readonly int current = InitialModel.CurrentId;
        private int x = 0;
        public List<Project> Projects { get; set; }

        public void OnGet()
		{
            if (InitialModel.CurrentRole.Equals("Creator"))
                Creator = Context.Creators.Include(c => c.ProjectsCreated).SingleOrDefault(c => c.Id == current);
            else if (InitialModel.CurrentRole.Equals("Backer"))
                Backer = Context.Backers.Include(b => b.ProjectsInvested).Include(b => b.Contributions).SingleOrDefault(b => b.Id == current);

            Projects = Context.Projects.ToList();
        }
	}
}