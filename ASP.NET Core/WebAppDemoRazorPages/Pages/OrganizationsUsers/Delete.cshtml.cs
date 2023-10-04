using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppDemoRazorPages.Data;

namespace WebAppDemoRazorPages.Pages.OrganizationsUsers
{
    public class DeleteModel : PageModel
    {
        private readonly WebAppDemoRazorPages.Data.ApplicationDbContext _context;

        public DeleteModel(WebAppDemoRazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public OrganizationUser OrganizationUser { get; set; } = default!;
        [BindProperty]
        public int? OrganizationId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? UserId,int organizationId)
        {
            OrganizationId = organizationId;
            if (UserId == null || _context.OrganizationUsers == null)
            {
                return NotFound();
            }

            var organizationuser = await _context.OrganizationUsers.FirstOrDefaultAsync(m => m.Id == UserId);

            if (organizationuser == null)
            {
                return NotFound();
            }
            else 
            {
                OrganizationUser = organizationuser;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? UserId)
        {
            if (UserId == null || _context.OrganizationUsers == null)
            {
                return NotFound();
            }
            var organizationuser = await _context.OrganizationUsers.FindAsync(UserId);

            if (organizationuser != null)
            {
                OrganizationUser = organizationuser;
                _context.OrganizationUsers.Remove(OrganizationUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Organizations/Details", new { id = OrganizationId });
        }
    }
}
