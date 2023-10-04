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
    public class OrganizationUsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrganizationUsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/OrganizationUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationUser>>> GetOrganizationUsers()
        {
          if (_context.OrganizationUsers == null)
          {
              return NotFound();
          }
            return await _context.OrganizationUsers.ToListAsync();
        }

        // GET: api/OrganizationUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationUser>> GetOrganizationUser(int id)
        {
          if (_context.OrganizationUsers == null)
          {
              return NotFound();
          }
            var organizationUser = await _context.OrganizationUsers.FindAsync(id);

            if (organizationUser == null)
            {
                return NotFound();
            }

            return organizationUser;
        }

        // PUT: api/OrganizationUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizationUser(int id, OrganizationUser organizationUser)
        {
            if (id != organizationUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(organizationUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationUserExists(id))
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

        // POST: api/OrganizationUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrganizationUser>> PostOrganizationUser(OrganizationUser organizationUser)
        {
          if (_context.OrganizationUsers == null)
          {
              return Problem("Entity set 'AppDbContext.OrganizationUsers'  is null.");
          }
            _context.OrganizationUsers.Add(organizationUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganizationUser", new { id = organizationUser.Id }, organizationUser);
        }

        // DELETE: api/OrganizationUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizationUser(int id)
        {
            if (_context.OrganizationUsers == null)
            {
                return NotFound();
            }
            var organizationUser = await _context.OrganizationUsers.FindAsync(id);
            if (organizationUser == null)
            {
                return NotFound();
            }

            _context.OrganizationUsers.Remove(organizationUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizationUserExists(int id)
        {
            return (_context.OrganizationUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
