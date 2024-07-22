using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nayan_Assignement3.Database.Nayan_Assignment3.Data;
using Nayan_Assignment3.Entities;

namespace Nayan_Assignment3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrdersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CustomerOrdersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/CustomerOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerOrder>>> GetCustomerOrders()
        {
            return await _context.CustomerOrders.ToListAsync();
        }

        // GET: api/CustomerOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerOrder>> GetCustomerOrder(int id)
        {
            var customerOrder = await _context.CustomerOrders.FindAsync(id);

            if (customerOrder == null)
            {
                return NotFound();
            }

            return customerOrder;
        }

        // GET: api/CustomerOrders/user/5
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<CustomerOrder>>> GetCustomerOrderByUserId(int id)
        {
            if (_context.CustomerOrders == null)
            {
                return NotFound();
            }
            var customerOrder = _context.CustomerOrders.Where(e => e.UserId == id);

            if (customerOrder == null)
            {
                return NotFound();
            }

            return await customerOrder.ToListAsync();
        }

        // PUT: api/CustomerOrders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerOrder(int id, CustomerOrder customerOrder)
        {
            if (id != customerOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerOrderExists(id))
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

        // POST: api/CustomerOrders
        [HttpPost]
        public async Task<ActionResult<CustomerOrder>> PostCustomerOrder(CustomerOrder customerOrder)
        {
            _context.CustomerOrders.Add(customerOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerOrder", new { id = customerOrder.Id }, customerOrder);
        }

        // DELETE: api/CustomerOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerOrder(int id)
        {
            var customerOrder = await _context.CustomerOrders.FindAsync(id);
            if (customerOrder == null)
            {
                return NotFound();
            }

            _context.CustomerOrders.Remove(customerOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerOrderExists(int id)
        {
            return _context.CustomerOrders.Any(e => e.Id == id);
        }
    }
}
