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
    public class PlanReviewsController : ControllerBase
    {
        /// <summary>
        /// Private field. Initialized with the PlanReviewsRepository and then has a constant reference (the field is readonly)
        /// </summary>
        private readonly IPlanReviewsRepository planReviewsRepo;

        /// <summary>
        /// PlanReviewsController. Manages planReviews calls to the database. Uses a wrapper for entity framework (PlanReviewsRepository). Dependency injection of the PlanReviewsRepository is done through startup.cs
        /// </summary>
        /// <param name="planReviewsRepository"></param>
        public PlanReviewsController(IPlanReviewsRepository planReviewsRepository)
        {
            planReviewsRepo = planReviewsRepository;
        }

        // GET: api/PlanReviews
        [HttpGet]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.PlanReviews), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<IEnumerable<Furs2Feathers.Domain.Models.PlanReviews>>> GetPlanReviews()
        {
            var list = await planReviewsRepo.ToListAsync();


            return Ok(list);
        }

        // GET: api/PlanReviews/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.PlanReviews), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.PlanReviews>> GetPlanReviews(int id)
        {
            var planReviews = await planReviewsRepo.FindAsync(id);

            if (planReviews == null)
            {
                return NotFound();
            }

            return Ok(planReviews);
        }

        // PUT: api/PlanReviews/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // from an update failing due to user error (id does not match any existing resource/database id for the entity)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // if something unexpectedly went wrong with the database or http request/response
        public async Task<IActionResult> PutPlanReviews(int id, Furs2Feathers.Domain.Models.PlanReviews planReviews)
        {
            if (id != planReviews.PlanReviewId)
            {
                return BadRequest();
            }

            /*_context.Entry(planReviews).State = EntityState.Modified;*/
            if (!await planReviewsRepo.ModifyStateAsync(planReviews, id))
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

        // POST: api/PlanReviews
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.PlanReviews), StatusCodes.Status201Created)] // successful post and returns created object
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.PlanReviews>> PostPlanReviews(Furs2Feathers.Domain.Models.PlanReviews planReviews)
        {
            planReviewsRepo.Add(planReviews);
            await planReviewsRepo.SaveChangesAsync();

            return CreatedAtAction("GetPlanReviews", new { id = planReviews.PlanReviewId }, planReviews);
        }

        // DELETE: api/PlanReviews/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.PlanReviews>> DeletePlanReviews(int id)
        {
            var planReviews = await planReviewsRepo.FindAsyncAsNoTracking(id); // get this planReviews matching this id
            // with tracking there are id errors even with just one row in the database so using AsNoTracking instead
            if (planReviews == null)
            {
                return NotFound();
            }

            planReviewsRepo.Remove(planReviews);
            await planReviewsRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
