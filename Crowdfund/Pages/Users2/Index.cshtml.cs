using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tsontarw_Razor.Data;
using Tsontarw_Razor.Domain;
using Microsoft.EntityFrameworkCore;


namespace Tsontarw_Razor.Pages.Users
{
    public class IndexModel : PageModel
    {
        private ProjectsDbContext Context { get; }
        public List<User> Users { get; set; }

        public IndexModel(ProjectsDbContext context)
        {
            Context = context;
        }

        public async Task OnGet()
        {
            Users = await Context.Users.ToListAsync();
        }
    }
}
