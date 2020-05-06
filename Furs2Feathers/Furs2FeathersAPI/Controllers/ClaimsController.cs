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
        /// <summary>
        /// Private field. Initialized with the ClaimsRepository and then has a constant reference (the field is readonly)
        /// </summary>
        private readonly IClaimsRepository claimsRepo;

        /// <summary>
        /// ClaimsController. Manages claims calls to the database. Uses a wrapper for entity framework (ClaimsRepository). Dependency injection of the ClaimsRepository is done through startup.cs
        /// </summary>
        /// <param name="claimsRepository"></param>
        public ClaimsController(IClaimsRepository claimsRepository)
        {
            claimsRepo = claimsRepository;
        }

        // GET: api/Claims
        [HttpGet]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Claims), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<IEnumerable<Furs2Feathers.Domain.Models.Claims>>> GetClaims()
        {
            var list = await claimsRepo.ToListAsync();


            return Ok(list);
        }

        // GET: api/Claims/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Claims), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Claims>> GetClaims(int id)
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
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // from an update failing due to user error (id does not match any existing resource/database id for the entity)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // if something unexpectedly went wrong with the database or http request/response
        public async Task<IActionResult> PutClaims(int id, Furs2Feathers.Domain.Models.Claims claims)
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
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Claims), StatusCodes.Status201Created)] // successful post and returns created object
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Claims>> PostClaims(Furs2Feathers.Domain.Models.Claims claims)
        {
            claimsRepo.Add(claims);
            await claimsRepo.SaveChangesAsync();

            return CreatedAtAction("GetClaims", new { id = claims.ClaimId }, claims);
        }

        // DELETE: api/Claims/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Claims>> DeleteClaims(int id)
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
