using DemoClients;

namespace WebAppDemoMVC.Models
{
    public class OrganizationIndexViewModel
    {
        public DirectionSearch DirectionSearch { get; set; }
        public string FieldName { get; set; } = "Name";
        public string Filter { get; set; } = default!;
        public List<string> AvaliableFields { get; set; } = new List<string>();
        public IEnumerable<Organization>? Organizations { get; set; }
    }
}
