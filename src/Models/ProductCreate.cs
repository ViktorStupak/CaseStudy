using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseStudy.WebApi.Models
{
    /// <summary>
    /// DTO object for creating new instance.
    /// </summary>
    public class ProductCreate
    {
        /// <summary>
        /// The name of the product
        /// </summary>
        /// <example>Men's basketball shoes</example>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the product's image URI.
        /// </summary>
        /// <value>
        /// The img URI.
        /// </value>
        /// <example>http\\test.com</example>
        [Required]
        public Uri ImgUri { get; set; }

        /// <summary>
        /// Get or set the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        /// <example>Description of the product</example>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Quantity left in stock
        /// </summary>
        /// <example>10</example>
        [Required]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// Converts to <see cref="Product"/>.
        /// </summary>
        /// <returns></returns>
        public Product ToProduct() => new Product
        {
            Name = this.Name,
            Price = this.Price,
            Description = this.Description,
            ImgUri = this.ImgUri
        };
    }
}