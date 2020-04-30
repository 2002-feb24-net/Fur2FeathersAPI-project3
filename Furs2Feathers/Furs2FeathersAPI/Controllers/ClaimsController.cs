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
    public class ClaimsController : ControllerBase
    {
        private readonly Furs2FeathersDbContext _context;

        public ClaimsController(Furs2FeathersDbContext context)
        {
            _context = context;
        }

        // GET: api/Claims
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Claims>>> GetClaims()
        {
            return await _context.Claims.ToListAsync();
        }

        // GET: api/Claims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Claims>> GetClaims(int id)
        {
            var claims = await _context.Claims.FindAsync(id);

            if (claims == null)
            {
                return NotFound();
            }

            return claims;
        }

        // PUT: api/Claims/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClaims(int id, Claims claims)
        {
            if (id != claims.ClaimId)
            {
                return BadRequest();
            }

            _context.Entry(claims).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaimsExists(id))
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

        // POST: api/Claims
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Claims>> PostClaims(Claims claims)
        {
            _context.Claims.Add(claims);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClaimsExists(claims.ClaimId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetClaims", new { id = claims.ClaimId }, claims);
        }

        // DELETE: api/Claims/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Claims>> DeleteClaims(int id)
        {
            var claims = await _context.Claims.FindAsync(id);
            if (claims == null)
            {
                return NotFound();
            }

            _context.Claims.Remove(claims);
            await _context.SaveChangesAsync();

            return claims;
        }

        private bool ClaimsExists(int id)
        {
            return _context.Claims.Any(e => e.ClaimId == id);
        }
    }
}
