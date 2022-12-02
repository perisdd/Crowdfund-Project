using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.InteropServices;
using Tsontarw_Razor.Domain;
using Tsontarw_Razor.Data;



namespace Tsontarw_Razor.Pages.Projects
{
    public class CreateModel : PageModel
    {

        [BindProperty] public Project Project { get; set; }
        [BindProperty] public List<int> UserIds { get; set; }
        public List<SelectListItem> UserSelectList { get; set; }
        private ProjectsDbContext Context { get; }
        public CreateModel(ProjectsDbContext context)
        {
            Context = context;
        }

        public async Task OnGet()
        {
            UserSelectList = await Context.Users.Select
                (a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.ToString()
                }).ToListAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            Project.Users = await Context.Users.Where(a => UserIds.Contains(a.Id)).ToListAsync();

            Context.Projects.Add(Project);
            await Context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}

