using Crowdfund.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Crowdfund.Domain;

namespace Crowdfund.Pages.Actors
{
    public class CreateModel : PageModel
    {
        private MoviesDbContext Context { get; }
        public int ActorCount { get; set; }
        [BindProperty] public Actor Actor { get; set; }

        public CreateModel(MoviesDbContext context)
        {
            Context = context;
        }

        public void OnGet()
        {
            ActorCount = Context.Actors.Count();
        }

        public async Task<ActionResult> OnPost() 
        {
            Context.Actors.Add(Actor);
            await Context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
