using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Crowdfund.Models;
using Crowdfund.DB;

namespace Crowdfund.Pages.Projects
{
    public class DetailsModel : PageModel
    {
		private FundDbContext Context { get; }

		public Project Project { get; set; }

		public Contribution Contribution { get; set; }

		public List<Backer> Backers { get; set; }

		public DetailsModel(FundDbContext context)
		{
			Context = context;
		}

		public void OnGet(int id)
        {
			Project = Context.Projects.
				Include(p => p.Rewards).
				Include(p => p.Backers).
				Include(p => p.Creator).
				SingleOrDefault(p => p.Id == id);

			Backers = Context.Backers.
				Include(b => b.ProjectsInvested).
				Include(b => b.Contributions).
				ToList();
		}

        public async Task<IActionResult> OnPostAsync(int id, Project project)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Context.Attach(Project).State = EntityState.Modified;

            if (id != project.Id)
            {
                return NotFound();
            }

            var backer = await Context.Backers
                .FirstAsync(b => b.Id == InitialModel.CurrentId);

            if (backer == null) { return NotFound(); }
          
            var proje = await Context.Projects.FindAsync(id);

            if(proje == null) { return NotFound(); }

            proje.Contributions = project.Contributions;
            proje.Backers.Add(backer);


            try
            {
                Context.Update(proje);
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(project.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["AlertMessage"] = "Project Updated Successfully!";
            return RedirectToPage("./Index");
        }

        private bool ProjectExists(int id)
        {
            return Context.Projects.Any(e => e.Id == id);
        }
    }
}
