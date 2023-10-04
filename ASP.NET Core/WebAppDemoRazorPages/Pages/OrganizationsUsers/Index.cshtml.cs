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
    public class IndexModel : PageModel
    {
        private readonly WebAppDemoRazorPages.Data.ApplicationDbContext _context;

        public IndexModel(WebAppDemoRazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OrganizationUser> OrganizationUser { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.OrganizationUsers != null)
            {
                OrganizationUser = await _context.OrganizationUsers
                .Include(o => o.Organization).ToListAsync();
            }
        }
    }
}
