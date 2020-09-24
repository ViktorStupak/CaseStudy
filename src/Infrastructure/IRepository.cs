using CaseStudy.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Infrastructure
{
    /// <summary>
    /// Base functionality of data get.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Get the paginated products.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public IAsyncEnumerable<Product> GetPaginatedProducts(int pageIndex, int pageSize = 10);

        /// <summary>
        /// Get the all products.
        /// </summary>
        /// <returns></returns>
        public IAsyncEnumerable<Product> GetProducts();

        /// <summary>
        /// Get the product by Id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<Product> GetProduct(long id);

        /// <summary>
        /// Post the product to database.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public Task<Product> PostProduct(ProductCreate product);

        /// <summary>
        /// Put the product to database.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public Task<IActionResult> PutProduct(Product product);

        /// <summary>
        /// Put the product's description to database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public Task<IActionResult> PutDescription(long id, string description);

        /// <summary>
        /// Indicate whether product exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<bool> ProductExists(long id);

    }
}
