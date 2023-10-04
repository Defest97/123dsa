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
    public class DetailsModel : PageModel
    {
        private readonly WebAppDemoRazorPages.Data.ApplicationDbContext _context;

        public DetailsModel(WebAppDemoRazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public OrganizationUser OrganizationUser { get; set; } = default!;
        [BindProperty]
        public int? OrganizationId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? userId,int organizationId)
        {
            OrganizationId=organizationId;
            if (userId == null || _context.OrganizationUsers == null)
            {
                return NotFound();
            }

            var organizationuser = await _context.OrganizationUsers.FirstOrDefaultAsync(m => m.Id == userId);
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
    }
}
