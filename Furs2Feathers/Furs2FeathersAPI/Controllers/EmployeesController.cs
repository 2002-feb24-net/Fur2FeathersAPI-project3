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
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// Private field. Initialized with the EmployeeRepository and then has a constant reference (the field is readonly)
        /// </summary>
        private readonly IEmployeeRepository employeeRepo;

        /// <summary>
        /// EmployeesController. Manages employee calls to the database. Uses a wrapper for entity framework (EmployeeRepository). Dependency injection of the EmployeeRepository is done through startup.cs
        /// </summary>
        /// <param name="employeeRepository"></param>
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            employeeRepo = employeeRepository;
        }

        // GET: api/Employees
        [HttpGet]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Employee), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<IEnumerable<Furs2Feathers.Domain.Models.Employee>>> GetEmployees()
        {
            var list = await employeeRepo.ToListAsync();


            return Ok(list);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Employee), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Employee>> GetEmployee(int id)
        {
            var employee = await employeeRepo.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // from an update failing due to user error (id does not match any existing resource/database id for the entity)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // if something unexpectedly went wrong with the database or http request/response
        public async Task<IActionResult> PutEmployee(int id, Furs2Feathers.Domain.Models.Employee employee)
        {
            if (id != employee.EmpId)
            {
                return BadRequest();
            }

            /*_context.Entry(employee).State = EntityState.Modified;*/
            if (!await employeeRepo.ModifyStateAsync(employee, id))
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

        // POST: api/Employees
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Employee), StatusCodes.Status201Created)] // successful post and returns created object
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Employee>> PostEmployee(Furs2Feathers.Domain.Models.Employee employee)
        {
            employeeRepo.Add(employee);
            await employeeRepo.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmpId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Employee>> DeleteEmployee(int id)
        {
            var employee = await employeeRepo.FindAsyncAsNoTracking(id); // get this employee matching this id
            // with tracking there are id errors even with just one row in the database so using AsNoTracking instead
            if (employee == null)
            {
                return NotFound();
            }

            employeeRepo.Remove(employee);
            await employeeRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
