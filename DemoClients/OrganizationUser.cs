using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoClients
{
    public class OrganizationUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        [MaxLength(10)]
        public string Code { get; set; } = default!;
        public Organization? Organization { get; set; }
        public int OrganizationId { get; set; }
        public UserAccess AccessLevel { get; set; }

    }
}
