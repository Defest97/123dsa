using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DemoClients;

namespace WebAppDemoRazorPages.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Organization>? Organizations { get; set; }
        public DbSet<OrganizationUser>? OrganizationUsers { get; set; }
    }
}