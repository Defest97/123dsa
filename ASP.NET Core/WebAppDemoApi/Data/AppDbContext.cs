using DemoClients;
using Microsoft.EntityFrameworkCore;

namespace WebAppDemoApi.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationUser> OrganizationUsers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }
    }
}
