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
    public class ProductCommentsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ProductCommentsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ProductComments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductComment>>> GetProductComments()
        {
            return await _context.ProductComments.ToListAsync();
        }

        // GET: api/ProductComments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductComment>> GetProductComment(int id)
        {
            var productComment = await _context.ProductComments.FindAsync(id);

            if (productComment == null)
            {
                return NotFound();
            }

            return productComment;
        }

        // GET: api/ProductComments/product/5
        [HttpGet("product/{id}")]
        public async Task<ActionResult<IEnumerable<ProductComment>>> GetProductCommentsByProductId(int id)
        {
            if (_context.ProductComments == null)
            {
                return NotFound();
            }
            var productComment = _context.ProductComments.Where(e => e.ProductId == id);

            if (productComment == null)
            {
                return NotFound();
            }

            return await productComment.ToListAsync();
        }

        // PUT: api/ProductComments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductComment(int id, ProductComment productComment)
        {
            if (id != productComment.Id)
            {
                return BadRequest();
            }

            _context.Entry(productComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCommentExists(id))
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

        // POST: api/ProductComments
        [HttpPost]
        public async Task<ActionResult<ProductComment>> PostProductComment(ProductComment productComment)
        {
            _context.ProductComments.Add(productComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductComment", new { id = productComment.Id }, productComment);
        }

        // DELETE: api/ProductComments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductComment(int id)
        {
            var productComment = await _context.ProductComments.FindAsync(id);
            if (productComment == null)
            {
                return NotFound();
            }

            _context.ProductComments.Remove(productComment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductCommentExists(int id)
        {
            return _context.ProductComments.Any(e => e.Id == id);
        }
    }
}
