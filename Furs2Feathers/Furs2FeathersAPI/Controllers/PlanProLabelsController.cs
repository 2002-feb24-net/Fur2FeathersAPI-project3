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
    public class PlanProLabelsController : ControllerBase
    {
        private readonly Furs2FeathersDbContext _context;

        public PlanProLabelsController(Furs2FeathersDbContext context)
        {
            _context = context;
        }

        // GET: api/PlanProLabels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanProLabels>>> GetPlanProLabels()
        {
            return await _context.PlanProLabels.ToListAsync();
        }

        // GET: api/PlanProLabels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlanProLabels>> GetPlanProLabels(int id)
        {
            var planProLabels = await _context.PlanProLabels.FindAsync(id);

            if (planProLabels == null)
            {
                return NotFound();
            }

            return planProLabels;
        }

        // PUT: api/PlanProLabels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanProLabels(int id, PlanProLabels planProLabels)
        {
            if (id != planProLabels.PlanProLabelsId)
            {
                return BadRequest();
            }

            _context.Entry(planProLabels).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanProLabelsExists(id))
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

        // POST: api/PlanProLabels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlanProLabels>> PostPlanProLabels(PlanProLabels planProLabels)
        {
            _context.PlanProLabels.Add(planProLabels);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlanProLabelsExists(planProLabels.PlanProLabelsId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlanProLabels", new { id = planProLabels.PlanProLabelsId }, planProLabels);
        }

        // DELETE: api/PlanProLabels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlanProLabels>> DeletePlanProLabels(int id)
        {
            var planProLabels = await _context.PlanProLabels.FindAsync(id);
            if (planProLabels == null)
            {
                return NotFound();
            }

            _context.PlanProLabels.Remove(planProLabels);
            await _context.SaveChangesAsync();

            return planProLabels;
        }

        private bool PlanProLabelsExists(int id)
        {
            return _context.PlanProLabels.Any(e => e.PlanProLabelsId == id);
        }
    }
}
