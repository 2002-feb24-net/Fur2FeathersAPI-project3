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
    public class PetsController : ControllerBase
    {
        /// <summary>
        /// Private field. Initialized with the PetRepository and then has a constant reference (the field is readonly)
        /// </summary>
        private readonly IPetRepository petRepo;

        /// <summary>
        /// PetsController. Manages pet calls to the database. Uses a wrapper for entity framework (PetRepository). Dependency injection of the PetRepository is done through startup.cs
        /// </summary>
        /// <param name="petRepository"></param>
        public PetsController(IPetRepository petRepository)
        {
            petRepo = petRepository;
        }

        // GET: api/Pets
        [HttpGet]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Pet), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<IEnumerable<Furs2Feathers.Domain.Models.Pet>>> GetPets()
        {
            var list = await petRepo.ToListAsync();


            return Ok(list);
        }

        // GET: api/Pets/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Pet), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Pet>> GetPet(int id)
        {
            var pet = await petRepo.FindAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        // GET: api/Pets/5
        [HttpGet("cust/{id}")]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Pet), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public ActionResult<Furs2Feathers.Domain.Models.Pet[]> GetCustPets(int id)
        {
            var pet = petRepo.FindByCustId(id);

            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }
        // PUT: api/Pets/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // from an update failing due to user error (id does not match any existing resource/database id for the entity)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // if something unexpectedly went wrong with the database or http request/response
        public async Task<IActionResult> PutPet(int id, Furs2Feathers.Domain.Models.Pet pet)
        {
            if (id != pet.PetId)
            {
                return BadRequest();
            }

            /*_context.Entry(pet).State = EntityState.Modified;*/
            if (!await petRepo.ModifyStateAsync(pet, id))
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

        // POST: api/Pets
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Pet), StatusCodes.Status201Created)] // successful post and returns created object
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Pet>> PostPet(Furs2Feathers.Domain.Models.Pet pet)
        {
            petRepo.Add(pet);
            await petRepo.SaveChangesAsync();

            return CreatedAtAction("GetPet", new { id = pet.PetId }, pet);
        }

        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Pet>> DeletePet(int id)
        {
            var pet = await petRepo.FindAsyncAsNoTracking(id); // get this pet matching this id
            // with tracking there are id errors even with just one row in the database so using AsNoTracking instead
            if (pet == null)
            {
                return NotFound();
            }

            petRepo.Remove(pet);
            await petRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
