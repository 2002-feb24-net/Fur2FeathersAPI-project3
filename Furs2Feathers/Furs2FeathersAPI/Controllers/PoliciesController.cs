using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Furs2Feathers.DataAccess.Models;

namespace Furs2FeathersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        private readonly Furs2FeathersDbContext _context;

        public PoliciesController(Furs2FeathersDbContext context)
        {
            _context = context;
        }

        // GET: api/Policies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Policies>>> GetPolicies()
        {
            return await _context.Policies.ToListAsync();
        }

        // GET: api/Policies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Policies>> GetPolicies(int id)
        {
            var policies = await _context.Policies.FindAsync(id);

            if (policies == null)
            {
                return NotFound();
            }

            return policies;
        }

        // PUT: api/Policies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPolicies(int id, Policies policies)
        {
            if (id != policies.PolicyId)
            {
                return BadRequest();
            }

            _context.Entry(policies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoliciesExists(id))
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

        // POST: api/Policies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Policies>> PostPolicies(Policies policies)
        {
            _context.Policies.Add(policies);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PoliciesExists(policies.PolicyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPolicies", new { id = policies.PolicyId }, policies);
        }

        // DELETE: api/Policies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Policies>> DeletePolicies(int id)
        {
            var policies = await _context.Policies.FindAsync(id);
            if (policies == null)
            {
                return NotFound();
            }

            _context.Policies.Remove(policies);
            await _context.SaveChangesAsync();

            return policies;
        }

        private bool PoliciesExists(int id)
        {
            return _context.Policies.Any(e => e.PolicyId == id);
        }
    }
}
