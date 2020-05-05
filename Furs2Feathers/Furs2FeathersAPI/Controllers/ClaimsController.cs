using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Furs2Feathers.Domain.Interfaces;

namespace Furs2FeathersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimsRepository claimsRepo;


        public ClaimsController(IClaimsRepository claimsRepository)
        {
            claimsRepo = claimsRepository;
        }

        // GET: api/Claims
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Furs2Feathers.Domain.Models.Claims>>> GetClaims()
        {
            var list = await claimsRepo.ToListAsync();


            return Ok(list);
        }

        // GET: api/Claims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Claims>> GetClaim(int id)
        {
            var claims = await claimsRepo.FindAsync(id);

            if (claims == null)
            {
                return NotFound();
            }

            return Ok(claims);
        }

        // PUT: api/Claims/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClaim(int id, Furs2Feathers.Domain.Models.Claims claims)
        {
            if (id != claims.ClaimId)
            {
                return BadRequest();
            }

            /*_context.Entry(claims).State = EntityState.Modified;*/
            if (!await claimsRepo.ModifyStateAsync(claims, id))
            {
                return NotFound();
                // if false, then modifying state failed
            }
            else
            {
                return NoContent();
                // successful put
            }
        }

        // POST: api/Claims
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Claims>> PostClaim(Furs2Feathers.Domain.Models.Claims claims)
        {
            claimsRepo.Add(claims);
            await claimsRepo.SaveChangesAsync();

            return CreatedAtAction("GetClaim", new { id = claims.ClaimId }, claims);
        }

        // DELETE: api/Claims/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Claims>> DeleteClaim(int id)
        {
            var claims = await claimsRepo.FindAsyncAsNoTracking(id); // get this claims matching this id
            // with tracking there are id errors even with just one row in the database so using AsNoTracking instead
            if (claims == null)
            {
                return NotFound();
            }

            claimsRepo.Remove(claims);
            await claimsRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
