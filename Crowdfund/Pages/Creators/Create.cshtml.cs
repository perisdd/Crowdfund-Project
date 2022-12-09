using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Crowdfund.Models;
using Crowdfund.DB;
using NToastNotify;

namespace Crowdfund.Pages.Creators
{
    public class CreateModel : PageModel
    {
        private FundDbContext Context { get; }

        private readonly IToastNotification _toastNotification;

        [BindProperty]
		public Creator Creator { get; set; }

        public CreateModel(FundDbContext context,  IToastNotification toastNotification)
        {
            Context = context;
            _toastNotification = toastNotification;
        }

        public void OnGet()
        { }

        public async Task<IActionResult> OnPost()
        {
            Context.Creators.Add(Creator);
            await Context.SaveChangesAsync();

            //TempData["AlertMessage"] = "Creator Added Successfully!";
            _toastNotification.AddSuccessToastMessage("Creator Added Successfully!");
            return RedirectToPage("./Index");
        }
    }
}
