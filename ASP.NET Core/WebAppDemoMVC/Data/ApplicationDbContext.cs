using DemoClients;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAppDemoMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Organization>? Organizations { get; set; }
        public DbSet<OrganizationUser>? OrganizationUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}