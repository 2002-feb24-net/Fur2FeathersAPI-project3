using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Furs2Feathers.DataAccess.Models;
using Furs2Feathers.Domain.Interfaces;

namespace Furs2FeathersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        /// <summary>
        /// Private field. Initialized with the PlanRepository and then has a constant reference (the field is readonly)
        /// </summary>
        private readonly IPlanRepository planRepo;

        /// <summary>
        /// PlansController. Manages plan calls to the database. Uses a wrapper for entity framework (PlanRepository). Dependency injection of the PlanRepository is done through startup.cs
        /// </summary>
        /// <param name="planRepository"></param>
        public PlansController(IPlanRepository planRepository)
        {
            planRepo = planRepository;
        }

        // GET: api/Plans
        [HttpGet]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Plan), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<IEnumerable<Furs2Feathers.Domain.Models.Plan>>> GetPlans()
        {
            var list = await planRepo.ToListAsync();


            return Ok(list);
        }

        // GET: api/Plans/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Plan), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Plan>> GetPlan(int id)
        {
            var plan = await planRepo.FindAsync(id);

            if (plan == null)
            {
                return NotFound();
            }

            return Ok(plan);
        }

        // PUT: api/Plans/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // from an update failing due to user error (id does not match any existing resource/database id for the entity)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // if something unexpectedly went wrong with the database or http request/response
        public async Task<IActionResult> PutPlan(int id, Furs2Feathers.Domain.Models.Plan plan)
        {
            if (id != plan.PlanId)
            {
                return BadRequest();
            }

            /*_context.Entry(plan).State = EntityState.Modified;*/
            if (!await planRepo.ModifyStateAsync(plan, id))
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

        // POST: api/Plans
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Plan), StatusCodes.Status201Created)] // successful post and returns created object
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Plan>> PostPlan(Furs2Feathers.Domain.Models.Plan plan)
        {
            planRepo.Add(plan);
            await planRepo.SaveChangesAsync();

            return CreatedAtAction("GetPlan", new { id = plan.PlanId }, plan);
        }

        // DELETE: api/Plans/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Plan>> DeletePlan(int id)
        {
            var plan = await planRepo.FindAsyncAsNoTracking(id); // get this plan matching this id
            // with tracking there are id errors even with just one row in the database so using AsNoTracking instead
            if (plan == null)
            {
                return NotFound();
            }

            planRepo.Remove(plan);
            await planRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
