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
        private readonly IAddressRepository claimsRepo;


        public AddressesController(IAddressRepository claimsRepository)
        {
            claimsRepo = claimsRepository;
        }

        // GET: api/Claims
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Furs2Feathers.Domain.Models.Address>>> GetAddress()
        {
            var list = await claimsRepo.ToListAsync();


            return Ok(list);
        }

        // GET: api/Claims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Address>> GetAddress(int id)
        {
            var address = await claimsRepo.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // PUT: api/Claims/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Furs2Feathers.Domain.Models.Address address)
        {
            if (id != address.AddressId)
            {
                return BadRequest();
            }

            /*_context.Entry(address).State = EntityState.Modified;*/
            if (!await claimsRepo.ModifyStateAsync(address, id))
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
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Address>> PostAddress(Furs2Feathers.Domain.Models.Address address)
        {
            claimsRepo.Add(address);
            await claimsRepo.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.AddressId }, address);
        }

        // DELETE: api/Claims/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Address>> DeleteAddress(int id)
        {
            var address = await claimsRepo.FindAsyncAsNoTracking(id); // get this address matching this id
            // with tracking there are id errors even with just one row in the database so using AsNoTracking instead
            if (address == null)
            {
                return NotFound();
            }

            claimsRepo.Remove(address);
            await claimsRepo.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return claimsRepo.Any(e => e.AddressId == id);
        }
    }
}
