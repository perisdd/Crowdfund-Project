using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Crowdfund.DB;
using Crowdfund.Models;
using NToastNotify;

namespace Crowdfund.Pages.Backers
{
    public class CreateModel : PageModel
    {
        private FundDbContext Context { get; }
        private readonly IToastNotification _toastNotification;

        [BindProperty]
        public Backer Backer { get; set; }

        public CreateModel(FundDbContext context, IToastNotification toastNotification)
        {
            Context = context;
            _toastNotification = toastNotification;
        }


        public void OnGet()
        { }

        public async Task<ActionResult> OnPostAsync()
        {
            Context.Backers.Add(Backer);
            await Context.SaveChangesAsync();
            _toastNotification.AddSuccessToastMessage("Backer Added Successfully!");
            //TempData["AlertMessage"] = "Backer Added Successfully!";
            return RedirectToPage("./Index");
        }
    }
}
