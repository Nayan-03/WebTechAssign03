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
    public class StoreProductsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public StoreProductsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/StoreProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreProduct>>> GetStoreProducts()
        {
            return await _context.StoreProducts.ToListAsync();
        }

        // GET: api/StoreProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreProduct>> GetStoreProduct(int id)
        {
            var storeProduct = await _context.StoreProducts.FindAsync(id);

            if (storeProduct == null)
            {
                return NotFound();
            }

            return storeProduct;
        }

        // PUT: api/StoreProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoreProduct(int id, StoreProduct storeProduct)
        {
            if (id != storeProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(storeProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreProductExists(id))
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

        // POST: api/StoreProducts
        [HttpPost]
        public async Task<ActionResult<StoreProduct>> PostStoreProduct(StoreProduct storeProduct)
        {
            _context.StoreProducts.Add(storeProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStoreProduct", new { id = storeProduct.Id }, storeProduct);
        }

        // DELETE: api/StoreProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoreProduct(int id)
        {
            var storeProduct = await _context.StoreProducts.FindAsync(id);
            if (storeProduct == null)
            {
                return NotFound();
            }

            _context.StoreProducts.Remove(storeProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreProductExists(int id)
        {
            return _context.StoreProducts.Any(e => e.Id == id);
        }
    }
}
