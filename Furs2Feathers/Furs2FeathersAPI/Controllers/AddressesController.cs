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
        /// <summary>
        /// Private field. Initialized with the AddressRepository and then has a constant reference (the field is readonly)
        /// </summary>
        private readonly IAddressRepository addressRepo;

        /// <summary>
        /// AddressController. Manages address calls to the database. Uses a wrapper for entity framework (AddressRepository). Dependency injection of the AddressRepository is done through startup.cs
        /// </summary>
        /// <param name="addressRepository"></param>
        public AddressesController(IAddressRepository addressRepository)
        {
            addressRepo = addressRepository;
        }

        // GET: api/Addresses
        [HttpGet]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Address), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<IEnumerable<Furs2Feathers.Domain.Models.Address>>> GetAddress()
        {
            var list = await addressRepo.ToListAsync();
            

            return Ok(list);
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Address), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
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
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // from an update failing due to user error (id does not match any existing resource/database id for the entity)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // if something unexpectedly went wrong with the database or http request/response
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
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Address), StatusCodes.Status201Created)] // successful post and returns created object
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Address>> PostAddress(Furs2Feathers.Domain.Models.Address address)
        {
            addressRepo.Add(address);
            await addressRepo.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.AddressId }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Address>> DeleteAddress(int id)
        {
            var address = await addressRepo.FindAsyncAsNoTracking(id); // get this address matching this id
            // with tracking there are id errors even with just one row in the database so using AsNoTracking instead
            if (address == null)
            {
                return NotFound();
            }

            addressRepo.Remove(address);
            await addressRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
