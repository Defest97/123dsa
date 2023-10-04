using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppDemoRazorPages.Data;

namespace WebAppDemoRazorPages.Pages.OrganizationsUsers
{
    public class EditModel : PageModel
    {
        private readonly WebAppDemoRazorPages.Data.ApplicationDbContext _context;

        public EditModel(WebAppDemoRazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrganizationUser OrganizationUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.OrganizationUsers == null)
            {
                return NotFound();
            }

            var organizationuser =  await _context.OrganizationUsers.FirstOrDefaultAsync(m => m.Id == id);
            if (organizationuser == null)
            {
                return NotFound();
            }
            OrganizationUser = organizationuser;
           ViewData["OrganizationId"] = new SelectList(_context.Organizations, "Id", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OrganizationUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationUserExists(OrganizationUser.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrganizationUserExists(int id)
        {
          return (_context.OrganizationUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
