using CaseStudy.WebApi.Infrastructure;
using CaseStudy.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Controllers
{
    /// <summary>
    /// Providing all available products of an eshop and enabling the partial update of one product
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ProductsController(IRepository repository)
        {
            _repository = repository;
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
            Log.Debug("start process All Product");
            return this._repository.GetProducts();
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
        public IAsyncEnumerable<Product> GetProducts(int pageIndex, int pageSize = 10)
        {
            Log.Debug($"start process All paginated products. page index: {pageIndex}; pagesize: {pageSize}");
            return this._repository.GetPaginatedProducts(pageIndex, pageSize);
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
            using (LogContext.PushProperty("ProductID", $"id: {id}"))
            {
                Log.Debug("start process get product.");
                var product = await this._repository.GetProduct(id).ConfigureAwait(false);
                Log.Debug("find product {@Product}", product);
                if (product != null)
                {
                    return this.Ok(product);
                }
                Log.Error("product not found");
                return NotFound();

            }

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
            using (LogContext.PushProperty("ProductID", $"id: {product.Id}"))
            {
                Log.Debug("receive product {@Product}", product);
                return await this._repository.PutProduct(product).ConfigureAwait(false);

            }
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
        public async Task<IActionResult> PutDescription(long id, [FromBody] ProductDescription description)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));
            if (string.IsNullOrWhiteSpace(description.Description)) return BadRequest("value of the description is empty");
            using (LogContext.PushProperty("ProductID", $"id: {id}"))
            {

                Log.Debug("receive description {@Description}", description);
                return await this._repository.PutDescription(id, description.Description).ConfigureAwait(false);
            }
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
            Log.Debug("receive product {@Product}", product);
            var productDb = await this._repository.PostProduct(product).ConfigureAwait(false);
            using (LogContext.PushProperty("ProductID", $"id: {productDb.Id}"))
            {
                Log.Debug("create product {@Product}", productDb);
                return CreatedAtAction("GetProduct", new { id = productDb.Id }, productDb);
            }
        }


    }
}
