using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Crowdfund.Models;
using Crowdfund.DB;
using NToastNotify;

namespace Crowdfund.Pages.Projects
{
    public class DetailsModel : PageModel
    {
		private FundDbContext Context { get; }
        private readonly IToastNotification _toastNotification;
        [BindProperty] public Project Project { get; set; }

		[BindProperty] public Contribution Contribution { get; set; }

		[BindProperty] public List<Backer> Backers { get; set; }

		public DetailsModel(FundDbContext context, IToastNotification toastNotification)
		{
			Context = context;
            _toastNotification = toastNotification;
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


		public void OnPost(int id)
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

			Contribution.Project = Project;
			Contribution.Backer = Context.Backers.SingleOrDefault(b => b.Id == InitialModel.CurrentId);

			Context.Backers.SingleOrDefault(b => b.Id == InitialModel.CurrentId).Contributions.Add(Contribution);
			Context.Backers.SingleOrDefault(b => b.Id == InitialModel.CurrentId).ProjectsInvested.Add(Project);
			Project.Contributions += Contribution.Amount;
            _toastNotification.AddSuccessToastMessage("Thank you for your contribution!");
            Context.Contributions.Add(Contribution);
			Context.SaveChanges();

		}
		/*
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
		*/
    }
}
