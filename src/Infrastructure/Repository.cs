using CaseStudy.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Infrastructure
{
    /// <summary>
    /// Connect to database according to the <see cref="DataContext"/>.
    /// </summary>
    /// <seealso cref="IRepository" />
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(DataContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Get the paginated products.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>List of the products for 1 page (length is pageSize, skip (pageIndex - 1) * pageSize)</returns>
        public PaginatedList<Product> GetPaginatedProducts(int pageIndex, int pageSize = 10)
        {
            return PaginatedList<Product>.CreateInstance(_context.Products.AsNoTracking(), pageIndex, pageSize);
        }

        /// <summary>
        /// Get the all products.
        /// </summary>
        /// <returns></returns>
        public IAsyncEnumerable<Product> GetProducts()
        {
            return _context.Products.AsNoTracking().AsAsyncEnumerable();
        }

        /// <summary>
        /// Get the product by Id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Product> GetProduct(long id)
        {
            return await _context.Products.FindAsync(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Post the product to database.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">product</exception>
        public async Task<Product> PostProduct(ProductCreate product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            var productDb = product.ToProduct();
            await _context.Products.AddAsync(productDb).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return productDb;
        }

        /// <summary>
        /// Put the product to database.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public async Task<IActionResult> PutProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(product.Id).ConfigureAwait(false))
                {
                    return new NotFoundResult();
                }

                throw;
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Put the product's description to database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public async Task<IActionResult> PutDescription(long id, string description)
        {
            var product = await _context.Products.FindAsync(id).ConfigureAwait(false);
            product.Description = description;

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(id).ConfigureAwait(false))
                {
                    return new NotFoundResult();
                }

                throw;
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Indicate whether product exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> ProductExists(long id)
        {
            return await _context.Products.AnyAsync(e => e.Id == id).ConfigureAwait(false);
        }
    }
}