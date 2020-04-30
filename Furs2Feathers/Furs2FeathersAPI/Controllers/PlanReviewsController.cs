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
    public class PlanReviewsController : ControllerBase
    {
        private readonly Furs2FeathersDbContext _context;

        public PlanReviewsController(Furs2FeathersDbContext context)
        {
            _context = context;
        }

        // GET: api/PlanReviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanReviews>>> GetPlanReviews()
        {
            return await _context.PlanReviews.ToListAsync();
        }

        // GET: api/PlanReviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlanReviews>> GetPlanReviews(int id)
        {
            var planReviews = await _context.PlanReviews.FindAsync(id);

            if (planReviews == null)
            {
                return NotFound();
            }

            return planReviews;
        }

        // PUT: api/PlanReviews/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanReviews(int id, PlanReviews planReviews)
        {
            if (id != planReviews.PlanReviewId)
            {
                return BadRequest();
            }

            _context.Entry(planReviews).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanReviewsExists(id))
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

        // POST: api/PlanReviews
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlanReviews>> PostPlanReviews(PlanReviews planReviews)
        {
            _context.PlanReviews.Add(planReviews);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlanReviewsExists(planReviews.PlanReviewId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlanReviews", new { id = planReviews.PlanReviewId }, planReviews);
        }

        // DELETE: api/PlanReviews/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlanReviews>> DeletePlanReviews(int id)
        {
            var planReviews = await _context.PlanReviews.FindAsync(id);
            if (planReviews == null)
            {
                return NotFound();
            }

            _context.PlanReviews.Remove(planReviews);
            await _context.SaveChangesAsync();

            return planReviews;
        }

        private bool PlanReviewsExists(int id)
        {
            return _context.PlanReviews.Any(e => e.PlanReviewId == id);
        }
    }
}
