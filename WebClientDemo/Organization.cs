using System.Collections.Generic;

namespace DemoClients
{
    public class Organization
    {
        public int Id { get; set; } //128bit
        public string Name { get; set; } // user frendly
        public string FullName { get; set; }
        public IEnumerable<OrganizationUser> OrganizationUsers { get; set; }

    }
}