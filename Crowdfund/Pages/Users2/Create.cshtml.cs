using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tsontarw_Razor.Domain;
using Tsontarw_Razor.Data;
using Microsoft.EntityFrameworkCore;

namespace Tsontarw_Razor.Pages.Users
{
    public class CreateModel : PageModel
    {
        private ProjectsDbContext Context { get; }
        public int UserCount { get; set; }
        [BindProperty] public User User { get; set; }

        public CreateModel(ProjectsDbContext context)
        {
            Context = context;
        }

        public void OnGet()
        {
            UserCount = Context.Users.Count();
        }

        public async Task<ActionResult> OnPost()
        {
            Context.Users.Add(User);
            await Context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
 }   
