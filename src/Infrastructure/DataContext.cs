using CaseStudy.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            Log.Debug("create DataContext ");
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
