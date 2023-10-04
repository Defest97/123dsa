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

namespace WebAppDemoRazorPages.Pages.Organizations
{
    public class IndexModel : PageModel
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

        public IList<Organization> Organization { get;set; } = default!;
        [BindProperty]
        public string Filter { get; set; }
        [BindProperty]
        public DirectionSearch DirectionSearch { get; set; }
        [BindProperty]
        public string FieldName { get; set; } = "Name";
        private List<string> AvaliableFields { get; set; } = new List<string>();
        public async Task OnGetAsync()
        {
            if (_context.Organizations != null)
            {
                Organization = await _context.Organizations.ToListAsync();
            }
        }
        public async Task OnPostFilterAsync()
        {
            //var query = _context.Organizations.AsQueryable();
            //if (Filter != null)
            //{

            //    Expression<Func<Organization, bool>> predicate = org => false;
            //    foreach (var field in AvaliableFields)
            //    {
            //        predicate = predicate.Or(org => EF.Property<string>(org, field).Contains(Filter));
            //    }
            //    query = query.Where(predicate);
            //}
            //Organization =await query.ToListAsync();   
            if (Filter != null)
            {
                var sql = "SELECT * FROM Organizations WHERE";
                for (int i = 0; i < AvaliableFields.Count; i++)
                {
                    sql += $" {AvaliableFields[i]} LIKE '%{Filter}%'";
                    if (i != AvaliableFields.Count - 1)
                        sql += " OR ";
                }
                Organization = await _context.Organizations.FromSqlRaw(sql).ToListAsync();
            }
        }


    }
}
