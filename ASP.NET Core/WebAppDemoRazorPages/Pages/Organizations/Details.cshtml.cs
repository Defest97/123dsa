using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppDemoRazorPages.Data;

namespace WebAppDemoRazorPages.Pages.Organizations
{
    public class DetailsModel : PageModel
    {
        private readonly WebAppDemoRazorPages.Data.ApplicationDbContext _context;

        public DetailsModel(WebAppDemoRazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Organization Organization { get; set; } = default!; 
      public List<OrganizationUser> OrganizationUsers { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Organizations == null)
            {
                return NotFound();
            }

            var organization = await _context.Organizations.FirstOrDefaultAsync(m => m.Id == id);
            var organizationUsers = await _context.OrganizationUsers.Where(user=> user.OrganizationId==id).ToListAsync();
            if (organization == null)
            {
                return NotFound();
            }
            else 
            {
                Organization = organization;
                OrganizationUsers = organizationUsers;
            }
            return Page();
        }
    }
}
