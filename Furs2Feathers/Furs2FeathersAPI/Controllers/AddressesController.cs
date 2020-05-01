using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
/*using Microsoft.EntityFrameworkCore;
using Furs2Feathers.DataAccess.Models;*/
using Furs2Feathers.Domain.Interfaces;
/*using Furs2Feathers.DataAccess;*/

namespace Furs2FeathersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressRepository addressRepo;


        public AddressesController(IAddressRepository addressRepository)
        {
            addressRepo = addressRepository;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Furs2Feathers.Domain.Models.Address>>> GetAddress()
        {
            var list = await addressRepo.ToListAsync();
            

            return Ok(list);
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Address>> GetAddress(int id)
        {
            var address = await addressRepo.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // PUT: api/Addresses/5
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
            if (! await addressRepo.ModifyStateAsync(address, id))
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

        // POST: api/Addresses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Address>> PostAddress(Furs2Feathers.Domain.Models.Address address)
        {
            addressRepo.Add(address);
            await addressRepo.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.AddressId }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Address>> DeleteAddress(int id)
        {
            var address = await addressRepo.FindAsync(id); // get this address matching this id
            if (address == null)
            {
                return NotFound();
            }

            addressRepo.Remove(address);
            await addressRepo.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return addressRepo.Any(e => e.AddressId == id);
        }
    }
}
