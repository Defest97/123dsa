using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoClients;
using WebAppDemoApi.Data;

namespace WebAppDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrganizationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Organizations
        [HttpGet]
        public async Task<ActionResult<QueryResult<Organization>>> GetOrganizations(int skip=0, int take=50)
        {
          if (_context.Organizations == null)
          {
              return NotFound();
          }
          QueryResult<Organization> result = new QueryResult<Organization> { Items = await _context.Organizations.Skip(skip).Take(take).ToListAsync(), Count = await _context.Organizations.CountAsync() };
          return result;
            //  return Ok(new QueryResult<Organization>{Items = await _context.Organizations.Skip(skip).Take(take).ToListAsync(),Count = await _context.Organizations.CountAsync()});
        }

        // GET: api/Organizations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetOrganization(int id)
        {
          if (_context.Organizations == null)
          {
              return NotFound();
          }
            var organization = await _context.Organizations.FindAsync(id);

            if (organization == null)
            {
                return NotFound();
            }

            return organization;
        }

        // PUT: api/Organizations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganization(int id, Organization organization)
        {
            if (id != organization.Id)
            {
                return BadRequest();
            }

            _context.Entry(organization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Organizations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Organization>> PostOrganization(Organization organization)
        {
          if (_context.Organizations == null)
          {
              return Problem("Entity set 'AppDbContext.Organizations'  is null.");
          }
            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganization", new { id = organization.Id }, organization);
        }

        // DELETE: api/Organizations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            if (_context.Organizations == null)
            {
                return NotFound();
            }
            var organization = await _context.Organizations.FindAsync(id);
            if (organization == null)
            {
                return NotFound();
            }

            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizationExists(int id)
        {
            return (_context.Organizations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
