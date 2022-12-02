using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.InteropServices;
using Crowdfund.Models;
using Crowdfund.DB;



namespace Crowdfund.Pages.Projects
{
    public class CreateModel : PageModel
    {

        [BindProperty] public Project Project { get; set; }
        [BindProperty] public List<int> UserIds { get; set; }
        public List<SelectListItem> UserSelectList { get; set; }
        private FundDbContext Context { get; }
        public CreateModel(FundDbContext context)
        {
            Context = context;
        }

        public async Task OnGet()
        {
            UserSelectList = await Context.Backers.Select
                (a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.ToString()
                }).ToListAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            Project.Backers = await Context.Backers.Where(a => UserIds.Contains(a.Id)).ToListAsync();

            Context.Projects.Add(Project);
            await Context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}

