using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tsontarw_Razor.Domain;
using Microsoft.EntityFrameworkCore;
using Tsontarw_Razor.Data;

namespace Tsontarw_Razor.Pages.Users
{
    public class DetailsModel : PageModel
    {
       
            private ProjectsDbContext Context { get; }
            public User? User { get; set; }
            public List<Project> UserProjects { get; set; }

            public DetailsModel(ProjectsDbContext context)
            {
                Context = context;
            }

            public async Task<ActionResult> OnGet(int id)
            {
                User = await Context.Users.SingleOrDefaultAsync(a => a.Id == id);
                if (User is null) return BadRequest();
                UserProjects = await Context.Projects.Where(m => m.Users.Contains(User)).ToListAsync();
                return Page();
            }

        
    }
}
