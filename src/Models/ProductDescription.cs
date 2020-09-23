using System.ComponentModel.DataAnnotations;

namespace CaseStudy.WebApi.Models
{
    /// <summary>
    /// DTO object for creating new instance.
    /// </summary>
    public class ProductDescription
    {
        /// <summary>
        /// Get or set the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        /// <example>Description of the product</example>
        [Required]
        public string Description { get; set; }
    }
}