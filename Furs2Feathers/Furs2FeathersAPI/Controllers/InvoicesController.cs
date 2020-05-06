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
    public class InvoicesController : ControllerBase
    {
        /// <summary>
        /// Private field. Initialized with the InvoiceRepository and then has a constant reference (the field is readonly)
        /// </summary>
        private readonly IInvoiceRepository invoiceRepo;

        /// <summary>
        /// InvoiceController. Manages invoice calls to the database. Uses a wrapper for entity framework (InvoiceRepository). Dependency injection of the InvoiceRepository is done through startup.cs
        /// </summary>
        /// <param name="invoiceRepository"></param>
        public InvoicesController(IInvoiceRepository invoiceRepository)
        {
            invoiceRepo = invoiceRepository;
        }

        // GET: api/Invoices
        [HttpGet]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Invoice), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<IEnumerable<Furs2Feathers.Domain.Models.Invoice>>> GetInvoices()
        {
            var list = await invoiceRepo.ToListAsync();


            return Ok(list);
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Invoice), StatusCodes.Status200OK)] // successful get request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Invoice>> GetInvoice(int id)
        {
            var invoice = await invoiceRepo.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        // PUT: api/Invoices/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // from an update failing due to user error (id does not match any existing resource/database id for the entity)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // if something unexpectedly went wrong with the database or http request/response
        public async Task<IActionResult> PutInvoice(int id, Furs2Feathers.Domain.Models.Invoice invoice)
        {
            if (id != invoice.InvoiceId)
            {
                return BadRequest();
            }

            /*_context.Entry(invoice).State = EntityState.Modified;*/
            if (!await invoiceRepo.ModifyStateAsync(invoice, id))
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

        // POST: api/Invoices
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(typeof(Furs2Feathers.Domain.Models.Invoice), StatusCodes.Status201Created)] // successful post and returns created object
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]  // if something unexpectedly went wrong with the database or http request/response
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Invoice>> PostInvoice(Furs2Feathers.Domain.Models.Invoice invoice)
        {
            invoiceRepo.Add(invoice);
            await invoiceRepo.SaveChangesAsync();

            return CreatedAtAction("GetInvoice", new { id = invoice.InvoiceId }, invoice);
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // success, nothing returned (works as intended, request fulfilled)
        [ProducesResponseType(StatusCodes.Status404NotFound)] // from query of an id that does not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Furs2Feathers.Domain.Models.Invoice>> DeleteInvoice(int id)
        {
            var invoice = await invoiceRepo.FindAsyncAsNoTracking(id); // get this invoice matching this id
            // with tracking there are id errors even with just one row in the database so using AsNoTracking instead
            if (invoice == null)
            {
                return NotFound();
            }

            invoiceRepo.Remove(invoice);
            await invoiceRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
