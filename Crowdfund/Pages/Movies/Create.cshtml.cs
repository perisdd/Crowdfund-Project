using Crowdfund.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Crowdfund.Domain;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Crowdfund.Pages.Movies
{
    public class CreateModel : PageModel
    {
        [BindProperty] public Movie Movie { get; set; }
        [BindProperty] public List<int> ActorIds { get; set; }
        public List<SelectListItem> ActorSelectList { get; set; }
        private MoviesDbContext Context { get; }
        public CreateModel(MoviesDbContext context)
        {
            Context = context;
        }

        public async Task OnGet()
        {
            ActorSelectList = await Context.Actors.Select
                (a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.ToString()
                }).ToListAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            Movie.Actors = await Context.Actors.Where(a => ActorIds.Contains(a.Id)).ToListAsync();

            Context.Movies.Add(Movie);
            await Context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
