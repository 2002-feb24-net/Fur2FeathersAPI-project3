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
    public class PlanProLabelsController : ControllerBase
    {
        /// <summary>
        /// Private field. Initialized with the PlanProLabelsRepository and then has a constant reference (the field is readonly)
        /// </summary>
        private readonly IPlanProLabelsRepository planProLabelsRepo;

        /// <summary>
        /// PlanProLabelsController. Manages planProLabels calls to the database. Uses a wrapper for entity framework (PlanProLabelsRepository). Dependency injection of the PlanProLabelsRepository is done through startup.cs
        /// </summary>
        /// <param name="planProLabelsRepository"></param>
        public PlanProLabelsController(IPlanProLabelsRepository planProLabelsRepository)
        {
            planProLabelsRepo = planProLabelsRepository;
        }

        // GET: api/PlanProLabels
        [HttpGet]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.PlanProLabels), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<IEnumerable<Furs2Feathers.Domain.Models.PlanProLabels>>> GetPlanProLabels()
        {
            var list = await planProLabelsRepo.ToListAsync();


            return Ok(list);
        }

        // GET: api/PlanProLabels/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.PlanProLabels), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.PlanProLabels>> GetPlanProLabels(int id)
        {
            var planProLabels = await planProLabelsRepo.FindAsync(id);

            if (planProLabels == null)
            {
                return NotFound();
            }

            return Ok(planProLabels);
        }

        // PUT: api/PlanProLabels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // from an update failing due to user error (id does not match any existing resource/database id for the entity)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // if something unexpectedly went wrong with the database or http request/response
        public async Task<IActionResult> PutPlanProLabels(int id, Furs2Feathers.Domain.Models.PlanProLabels planProLabels)
        {
            if (id != planProLabels.PlanProLabelsId)
            {
                return BadRequest();
            }

            /*_context.Entry(planProLabels).State = EntityState.Modified;*/
            if (!await planProLabelsRepo.ModifyStateAsync(planProLabels, id))
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

        // POST: api/PlanProLabels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.PlanProLabels), StatusCodes.Status201Created)] // successful post and returns created object
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.PlanProLabels>> PostPlanProLabels(Furs2Feathers.Domain.Models.PlanProLabels planProLabels)
        {
            planProLabelsRepo.Add(planProLabels);
            await planProLabelsRepo.SaveChangesAsync();

            return CreatedAtAction("GetPlanProLabels", new { id = planProLabels.PlanProLabelsId }, planProLabels);
        }

        // DELETE: api/PlanProLabels/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.PlanProLabels>> DeletePlanProLabels(int id)
        {
            var planProLabels = await planProLabelsRepo.FindAsyncAsNoTracking(id); // get this planProLabels matching this id
            // with tracking there are id errors even with just one row in the database so using AsNoTracking instead
            if (planProLabels == null)
            {
                return NotFound();
            }

            planProLabelsRepo.Remove(planProLabels);
            await planProLabelsRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
