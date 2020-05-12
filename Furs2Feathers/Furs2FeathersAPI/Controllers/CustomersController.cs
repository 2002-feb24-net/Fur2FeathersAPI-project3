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
    public class CustomersController : ControllerBase
    {
        /// <summary>
        /// Private field. Initialized with the CustomerRepository and then has a constant reference (the field is readonly)
        /// </summary>
        private readonly ICustomerRepository customerRepo;

        /// <summary>
        /// CustomerController. Manages customer calls to the database. Uses a wrapper for entity framework (CustomerRepository). Dependency injection of the CustomerRepository is done through startup.cs
        /// </summary>
        /// <param name="customerRepository"></param>
        public CustomersController(ICustomerRepository customerRepository)
        {
            customerRepo = customerRepository;
        }

        // GET: api/Customers
        [HttpGet]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Customer), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<IEnumerable<Furs2Feathers.Domain.Models.Customer>>> GetCustomers()
        {
            var list = await customerRepo.ToListAsync();


            return Ok(list);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        [Route("")]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Customer), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Customer>> GetCustomer(int id)
        {
            //int num;
            
            //if (Int32.TryParse(idOrEmail, out num))
            //{
                var customer = await customerRepo.FindAsync(id);

                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            //}
            //else
            //   return GetCustomerByEmail(idOrEmail);
        }

        // GET: api/Customers/5
        [HttpGet("{email}")]
        [Route("GetCustomerByEmail")]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Customer), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public  ActionResult<Furs2Feathers.Domain.Models.Customer> GetCustomerByEmail(string email)
        {
            var customer = customerRepo.FindAsync(e => e.Email == email);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // from an update failing due to user error (id does not match any existing resource/database id for the entity)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // if something unexpectedly went wrong with the database or http request/response
        public async Task<IActionResult> PutCustomer(int id, Furs2Feathers.Domain.Models.Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            /*_context.Entry(customer).State = EntityState.Modified;*/
            if (!await customerRepo.ModifyStateAsync(customer, id))
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

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Customer), StatusCodes.Status201Created)] // successful post and returns created object
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Customer>> PostCustomer(Furs2Feathers.Domain.Models.Customer customer)
        {
            customerRepo.Add(customer);
            await customerRepo.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Customer>> DeleteCustomer(int id)
        {
            var customer = await customerRepo.FindAsyncAsNoTracking(id); // get this customer matching this id
            // with tracking there are id errors even with just one row in the database so using AsNoTracking instead
            if (customer == null)
            {
                return NotFound();
            }

            customerRepo.Remove(customer);
            await customerRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
