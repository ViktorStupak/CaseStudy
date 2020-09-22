using CaseStudy.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the products.
        /// </summary>
        /// <returns>All <see cref="Product"/> in the BD</returns>
        /// <response code="200">Returns if success</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public IAsyncEnumerable<Product> GetProducts()
        {
            return (IAsyncEnumerable<Product>)_context.Products.AsNoTracking();
        }

        /// <summary>
        /// Get the paginated products list.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        /// <response code="200">Returns if success</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ApiExplorerSettings(GroupName = "v2")]
        [HttpGet("{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetProducts(int pageIndex, int pageSize = 10)
        {
            var result = await PaginatedList<Product>.CreateAsync(_context.Products.AsNoTracking(), pageIndex, pageSize)
                .ConfigureAwait(false);
            return this.Ok(result);
        }

        /// <summary>
        /// Get the product by Id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <response code="200">Returns if success</response>
        /// <response code="404">If the product with id is not exist</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            var product = await _context.Products.FindAsync(id).ConfigureAwait(false);

            if (product == null)
            {
                return NotFound();
            }

            return this.Ok(product);
        }

        /// <summary>
        /// Put the product to DB. Modify according to request.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        /// <response code="204">Returns if update is success</response>
        /// <response code="400">If the description is null</response>
        /// <response code="404">If the product with id is not exist</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public async Task<IActionResult> PutProduct(Product product)
        {
            if (product == null) return BadRequest();

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Puts the description.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        /// <response code="204">Returns if update is success</response>
        /// <response code="400">If the description is null</response>
        /// <response code="404">If the product with id is not exist</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("description/{id}")]
        public async Task<IActionResult> PutDescription(long id, string description)
        {
            if (string.IsNullOrWhiteSpace(description)) return BadRequest($"value of the description is empty");

            var product = await _context.Products.FindAsync(id).ConfigureAwait(false);
            product.Description = description;

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a Product.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "name": "Men's basketball shoes",
        ///         "imgUri": "string",
        ///        "description": "string",
        ///        "price": 10
        ///     }
        ///
        /// </remarks>
        /// <param name="product"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created <see cref="Product"/></response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> PostProduct(ProductCreate product)
        {
            if (product == null) return BadRequest();

            var productDb = product.ToProduct();
            await _context.Products.AddAsync(productDb).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetProduct", new { id = productDb.Id }, productDb);
        }

        private bool ProductExists(long id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
