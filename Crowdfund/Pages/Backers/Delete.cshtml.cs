using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crowdfund.DB;
using Crowdfund.Models;

namespace Crowdfund.Pages.Backers
{
    public class DeleteModel : PageModel
    {
        private readonly FundDbContext _context;


        [BindProperty] public Backer Backer { get; set; }


        public DeleteModel(FundDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Backers == null)
            {
                return NotFound();
            }

            var backer = await _context.Backers
                .FirstOrDefaultAsync(m => m.Id == id);

            if (backer == null)
            {
                return NotFound();
            }
            else 
            {
                 Backer = backer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Backers == null)
            {
                return NotFound();
            }
            var backer = await _context.Backers.FindAsync(id);

            if (backer != null)
            {
                Backer = backer;
                _context.Backers.Remove(backer);
                await _context.SaveChangesAsync();
            }

            TempData["AlertMessage"] = "Backer Deleted Successfully!";
            return RedirectToPage("./Index");
        }
    }
}
