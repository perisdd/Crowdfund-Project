using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Crowdfund.Models;
using Crowdfund.DB;

namespace Crowdfund.Pages.Projects
{
    public class CreateModel : PageModel
    {
        [BindProperty] public Project Project { get; set; }
        [BindProperty] public List<int> BackerIds { get; set; }
        [BindProperty] public List<int> CreatorIds { get; set; }
        public List<SelectListItem> BackerSelectList { get; set; }
        public List<SelectListItem> CreatorSelectList { get; set; }
        private FundDbContext Context { get; }
        public CreateModel(FundDbContext context)
        {
            Context = context;
        }

        //public async Task OnGet()
        //{
        //    BackerSelectList = await Context.Backers.Select
        //        (a => new SelectListItem
        //        {
        //            Value = a.Id.ToString(),
        //            Text = a.ToString()
        //        }).ToListAsync();
        //}

        ////*******************COOKIE CREATOR -------------------PROBLEM IF MOVED HERE----------------------
        //public static int test { get; set; }

        //public IActionResult OnPost(int creators)
        //{
        //    test = creators;
        //    return RedirectToPage("Projects/Create");
        //}
        //public async Task OnGet()
        //{
        //    CreatorSelectList = await Context.Creators.Select
        //        (a => new SelectListItem
        //        {
        //            Value = a.Id.ToString(),
        //            Text = a.ToString()
        //        }).ToListAsync();
        //}
        ////*******************COOKIE CREATOR**********************-----------------------
        
        
        //public async Task<IActionResult> OnPost()
        //{
        //    Project.Backers = await Context.Backers.Where(a => BackerIds.Contains(a.Id)).ToListAsync();

        //    Context.Projects.Add(Project);
        //    await Context.SaveChangesAsync();
        //    return RedirectToPage("/Index");
        //}

        public async Task<IActionResult> OnPost()
        {
			int id = InitialModel.CurrentId;
			Project.Creator = await Context.Creators.SingleOrDefaultAsync(c => c.Id == id);

            Context.Projects.Add(Project);
            await Context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}

