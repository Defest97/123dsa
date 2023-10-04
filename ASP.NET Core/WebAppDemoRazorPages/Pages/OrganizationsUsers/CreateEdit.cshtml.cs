using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoClients;
using WebAppDemoRazorPages.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAppDemoRazorPages.Pages.OrganizationsUsers
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int organizationId, int? UserId, bool edit)
        {
            Edit = edit;
            OrganizationId= organizationId;
            if (!Edit)
            {
                OrganizationUser = new OrganizationUser { OrganizationId = OrganizationId };
                ViewData["CreatEdit"]="Create";
            }
            else
            {
                if (UserId == null || _context.OrganizationUsers == null)
                {
                    return NotFound();
                }

                var organizationUser = _context.OrganizationUsers.Find(UserId);
                if (organizationUser == null)
                {
                    return NotFound();
                }
                OrganizationUser = organizationUser;
                ViewData["CreatEdit"] = "Edit";
            }
            ViewData["OrganizationName"] = _context.Organizations.Find(organizationId).Name;
            return Page();
        }

        [BindProperty]
        public OrganizationUser OrganizationUser { get; set; } = default!;
        [BindProperty]
        public bool Edit { get; set; }
        [BindProperty]
        public int OrganizationId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Edit)
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

                return RedirectToPage("/Organizations/Details", new { id = OrganizationId });

            }
            else
            {
                if (!ModelState.IsValid || _context.OrganizationUsers == null || OrganizationUser == null)
                {
                    return Page();
                }
                _context.OrganizationUsers.Add(OrganizationUser);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Organizations/Details", new { id = OrganizationId });

        }

        private bool OrganizationUserExists(int id)
        {
            return (_context.OrganizationUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
