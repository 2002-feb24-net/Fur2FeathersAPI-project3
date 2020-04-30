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
    public class AddressesController : ControllerBase
    {
        private readonly f2fdbContext _context;
        private readonly IAddressRepository addressRepo;

        // This controller is decoupled from DbContext except for the PUT method


        public AddressesController(f2fdbContext context, IAddressRepository locationRepository)
        {
            _context = context;
            addressRepo = locationRepository;
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

       /* // PUT: api/Addresses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.AddressId)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await addressRepo.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

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
            var address = await addressRepo.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            addressRepo.Remove(address);
            await addressRepo.SaveChangesAsync();

            return Ok(address); //should be 204
        }

        private bool AddressExists(int id)
        {
            return addressRepo.Any(e => e.AddressId == id);
        }
    }
}
