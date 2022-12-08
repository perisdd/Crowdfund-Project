using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Crowdfund.DB;
using Crowdfund.Models;

namespace Crowdfund.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly FundDbContext _context;

        public EditModel(FundDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; } = default!;

        private async Task<bool> IsMyProject(int? projid, int currentuserId)
        {
            if (projid == null)
            {
                return false;
            }
            return await _context.Projects.AnyAsync(pr => pr.Id == projid && pr.CreatorId == currentuserId);
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var currentuserId = InitialModel.CurrentId;

            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var ismyProject = await IsMyProject(id, currentuserId);
            if (ismyProject == false)
            {
                return RedirectToPage("./Index");
            }

            var project = await _context.Projects.FirstAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            Project = project;
 
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id, Project project)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Project).State = EntityState.Modified;

            if (id != project.Id)
            {
                return NotFound();
            }

            var currentuserId = InitialModel.CurrentId;

            var ismyProject = await IsMyProject(id, currentuserId);


            if (ismyProject == false)
            {
                return RedirectToPage("./Index");
            }

            var myproject = await _context.Projects.FindAsync(id);
            
            myproject.Id = id;
            myproject.Title = project.Title;
            myproject.Description = project.Description;
            myproject.Contributions = project.Contributions;
            myproject.ProjectCategory = project.ProjectCategory;
            myproject.CreatorId = currentuserId;


            try
            {
                _context.Update(myproject);
                await _context.SaveChangesAsync();
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
          return _context.Projects.Any(e => e.Id == id);
        }


    }
}
