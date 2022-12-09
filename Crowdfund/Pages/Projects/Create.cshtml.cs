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
    public class CreateModel : PageModel
    {
        [BindProperty] public Project Project { get; set; }
        [BindProperty] public List<int> BackerIds { get; set; }
        [BindProperty] public List<int> CreatorIds { get; set; }
        public List<SelectListItem> BackerSelectList { get; set; }
        public List<SelectListItem> CreatorSelectList { get; set; }

		[BindProperty] public Reward Reward1 { get; set; }
		[BindProperty] public Reward Reward2 { get; set; }
		[BindProperty] public Reward Reward3 { get; set; }

		private FundDbContext Context { get; }
        private readonly IToastNotification _toastNotification;
        public CreateModel(FundDbContext context, IToastNotification toastNotification)
        {
            Context = context;
            _toastNotification = toastNotification;
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
			Project.CreationDate = DateTime.Now;

			Reward1.Title = "Tier 1";
			Reward2.Title = "Tier 2";
			Reward3.Title = "Tier 3";

			Project.Rewards.Add(Reward1);
			Project.Rewards.Add(Reward2);
			Project.Rewards.Add(Reward3);

			Context.Rewards.Add(Reward1);
			Context.Rewards.Add(Reward2);
			Context.Rewards.Add(Reward3);

			Context.Projects.Add(Project);
            await Context.SaveChangesAsync();
            _toastNotification.AddSuccessToastMessage("Project Created Successfully");
            TempData["AlertMessage"] = "Project Created Successfully!";
            return RedirectToPage("Index");
        }
    }
}

