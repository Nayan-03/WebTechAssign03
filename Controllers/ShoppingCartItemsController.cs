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
    public class ShoppingCartItemsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ShoppingCartItemsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingCartItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartItem>>> GetShoppingCartItems()
        {
            return await _context.ShoppingCartItems.ToListAsync();
        }

        // GET: api/ShoppingCartItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartItem>> GetShoppingCartItem(int id)
        {
            var shoppingCartItem = await _context.ShoppingCartItems.FindAsync(id);

            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            return shoppingCartItem;
        }

        // GET: api/ShoppingCartItems/cart/5
        [HttpGet("cart/{id}")]
        public async Task<ActionResult<IEnumerable<ShoppingCartItem>>> GetShoppingCartItems(int id)
        {
            var shoppingCartItems = _context.ShoppingCartItems.Where(c => c.CartId == id);

            if (shoppingCartItems == null)
            {
                return NotFound();
            }

            return await shoppingCartItems.ToListAsync();
        }

        // PUT: api/ShoppingCartItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingCartItem(int id, ShoppingCartItem shoppingCartItem)
        {
            if (id != shoppingCartItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingCartItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartItemExists(id))
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

        // POST: api/ShoppingCartItems
        [HttpPost]
        public async Task<ActionResult<ShoppingCartItem>> PostShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            _context.ShoppingCartItems.Add(shoppingCartItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingCartItem", new { id = shoppingCartItem.Id }, shoppingCartItem);
        }

        // DELETE: api/ShoppingCartItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCartItem(int id)
        {
            var shoppingCartItem = await _context.ShoppingCartItems.FindAsync(id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            _context.ShoppingCartItems.Remove(shoppingCartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingCartItemExists(int id)
        {
            return _context.ShoppingCartItems.Any(e => e.Id == id);
        }
    }
}
