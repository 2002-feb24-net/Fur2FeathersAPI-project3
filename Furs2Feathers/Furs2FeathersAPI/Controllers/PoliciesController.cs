using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Furs2Feathers.DataAccess.Models;
using Furs2Feathers.DataAccess.Repositories;

namespace Furs2FeathersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        /// <summary>
        /// Private field. Initialized with the PoliciesRepository and then has a constant reference (the field is readonly)
        /// </summary>
        private readonly IPoliciesRepository policiesRepo;

        /// <summary>
        /// PoliciesController. Manages policies calls to the database. Uses a wrapper for entity framework (PoliciesRepository). Dependency injection of the PoliciesRepository is done through startup.cs
        /// </summary>
        /// <param name="policiesRepository"></param>
        public PoliciesController(IPoliciesRepository policiesRepository)
        {
            policiesRepo = policiesRepository;
        }

        // GET: api/Policies
        [HttpGet]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Policies), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<IEnumerable<Furs2Feathers.Domain.Models.Policies>>> GetPolicies()
        {
            var list = await policiesRepo.ToListAsync();


            return Ok(list);
        }

        // GET: api/Policies/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Policies), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Policies>> GetPolicies(int id)
        {
            var policies = await policiesRepo.FindAsync(id);

            if (policies == null)
            {
                return NotFound();
            }

            return Ok(policies);
        }

        // PUT: api/Policies/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // from an update failing due to user error (id does not match any existing resource/database id for the entity)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // if something unexpectedly went wrong with the database or http request/response
        public async Task<IActionResult> PutPolicies(int id, Furs2Feathers.Domain.Models.Policies policies)
        {
            if (id != policies.PolicyId)
            {
                return BadRequest();
            }

            /*_context.Entry(policies).State = EntityState.Modified;*/
            if (!await policiesRepo.ModifyStateAsync(policies, id))
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

        // POST: api/Policies
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Policies), StatusCodes.Status201Created)] // successful post and returns created object
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Policies>> PostPolicies(Furs2Feathers.Domain.Models.Policies policies)
        {
            policiesRepo.Add(policies);
            await policiesRepo.SaveChangesAsync();

            return CreatedAtAction("GetPolicies", new { id = policies.PolicyId }, policies);
        }

        // DELETE: api/Policies/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Policies>> DeletePolicies(int id)
        {
            var policies = await policiesRepo.FindAsyncAsNoTracking(id); // get this policies matching this id
            // with tracking there are id errors even with just one row in the database so using AsNoTracking instead
            if (policies == null)
            {
                return NotFound();
            }

            policiesRepo.Remove(policies);
            await policiesRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
