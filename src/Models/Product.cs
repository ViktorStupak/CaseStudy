using System.ComponentModel.DataAnnotations;

namespace CaseStudy.WebApi.Models
{
    /// <summary>
    /// Main type of the product.
    /// </summary>
    /// <seealso cref="CaseStudy.WebApi.Models.ProductCreate" />
    public sealed class Product : ProductCreate
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        /// <example>58</example>
        [Required]
        public long Id { get; set; }
    }
}