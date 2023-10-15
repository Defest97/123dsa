using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppDemoRazorPages.Data;
using System.Linq.Expressions;
using LinqKit;
using WebAppDemoRazorPages.Models;

namespace WebAppDemoRazorPages.Pages.Organizations
{
    public class IndexModel : PageModel,IFilterable,IPaginated, ISortable
    {
        private readonly WebAppDemoRazorPages.Data.ApplicationDbContext _context;

        public IndexModel(WebAppDemoRazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
            foreach (var field in typeof(Organization).GetProperties())
            {
                if (field.PropertyType == typeof(string))
                    AvaliableFields.Add(field.Name);
            }
        }

        public IList<Organization> Organization { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }
        [BindProperty]
        public Organization OrganizationDelete { get; set; } = default!;
        private List<string> AvaliableFields { get; set; } = new List<string>();
        [BindProperty(SupportsGet = true)]
        public string Sort { get; set; } = string.Empty;
        [BindProperty(SupportsGet =true)]
        public int Skip { get; set; } = 0;
        public int CountItems { get; set; }
        public int PageSize { get; set; } = 12;

        async Task GetItems(IQueryable<Organization> query)
        {
            if (Filter != null)
            {

                Expression<Func<Organization, bool>> predicate = org => false;
                foreach (var field in AvaliableFields)
                {
                    predicate = predicate.Or(org => EF.Property<string>(org, field).Contains(Filter));
                }
                query = query.Where(predicate);
            }
            CountItems = await query.CountAsync();
            Sorting(ref query);
            Organization = await query.Skip(Skip).Take(PageSize).ToListAsync();
        }
        void Sorting(ref IQueryable<Organization> query)
        {
            switch (Sort)
            {
                case "Name":
                    {
                        query = query.OrderBy(org => org.Name);
                    }
                    break;
                case "NameDesending":
                    {
                        query = query.OrderByDescending(org => org.Name);
                    }
                    break;
                case "Fullname":
                    {
                        query = query.OrderBy(org => org.FullName);
                    }
                    break;
                case "FullnameDesending":
                    {
                        query = query.OrderByDescending(org => org.FullName);
                    }
                    break;
                case "Created":
                    {
                        query = query.OrderBy(org => org.Created);
                    }
                    break;
                case "CreatedDesending":
                    {
                        query = query.OrderByDescending(org => org.Created);
                    }
                    break;
                default:
                    {
                        query = query.OrderBy(org => org.Id);
                    }
                    break;
            }
        }
        public async Task OnGetAsync()
        {
            if (_context.Organizations != null)
            {
                //GenerateAndAddOrganizations();
                //_context.Organizations.RemoveRange(_context.Organizations.ToList());
                //_context.SaveChanges();
                await GetItems(_context.Organizations.AsQueryable());
            }
        }

        public void GenerateAndAddOrganizations()
        {
            Random random = new Random();

            for (int i = 0; i < 33333; i++)
            {
                Organization organization = new Organization
                {
                    Name = GenerateRandomName(),
                    FullName = GenerateRandomFullName(),
                    Created = GenerateRandomDate(),
                };

                _context.Organizations.Add(organization);
            }

            _context.SaveChanges();
        }
        private string GenerateRandomName()
        {
            // Логіка для генерації рандомного імені
            return "RandomName" + Guid.NewGuid().ToString().Substring(0, 8);
        }

        private string GenerateRandomFullName()
        {
            // Логіка для генерації рандомної повної назви
            return "RandomFullName" + Guid.NewGuid().ToString().Substring(0, 8);
        }

        private DateTime GenerateRandomDate()
        {
            Random random = new Random();
            // Генерація рандомної дати в діапазоні, наприклад, останні 5 років
            int days = random.Next(1, 1825); // 5 років в днях
            return DateTime.Now.AddDays(-days);
        }
        public async Task OnPostDeleteAsync(int? id)
        {
            var organization = await _context.Organizations.FindAsync(id);

            if (organization != null)
            {
                OrganizationDelete = organization;
                _context.Organizations.Remove(OrganizationDelete);
                await _context.SaveChangesAsync();
            }
            await GetItems(_context.Organizations.AsQueryable());
        }
        public async Task OnPostFilterAsync()
        {
            if (Filter != null)
            {
                var query = _context.Organizations.AsQueryable();
                
                await GetItems(query);
            }
        }
    }
}
