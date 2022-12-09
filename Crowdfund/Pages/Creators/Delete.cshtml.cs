using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crowdfund.DB;
using Crowdfund.Models;

namespace Crowdfund.Pages.Creators
{
    public class DeleteModel : PageModel
    {
        private readonly FundDbContext _context;


        [BindProperty] public Creator Creator { get; set; }


        public DeleteModel(FundDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Creators == null)
            {
                return NotFound();
            }

            var creator = await _context.Creators
                .FirstOrDefaultAsync(m => m.Id == id);

            if (creator == null)
            {
                return NotFound();
            }
            else 
            {
                 Creator = creator;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Creators == null)
            {
                return NotFound();
            }
            var creator = await _context.Creators.FindAsync(id);

            if (creator != null)
            {
                Creator = creator;
                _context.Creators.Remove(creator);
                await _context.SaveChangesAsync();
            }

            TempData["AlertMessage"] = "Project Deleted Successfully!";
            return RedirectToPage("./Index");
        }
    }
}
