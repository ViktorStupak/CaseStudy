using CaseStudy.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy.WebApi.Infrastructure
{
    /// <summary>
    /// Context of the DB.
    /// </summary>
    /// <seealso cref="DbContext" />
    public sealed class DataContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        /// <param name="opts">The opts.</param>
        public DataContext(DbContextOptions<DataContext> opts) : base(opts)
        {
            this.Database.EnsureCreated();
        }

        /// <summary>
        /// Get or set the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public DbSet<Product> Products { get; set; }
    }
}
