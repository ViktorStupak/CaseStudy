using System;
using System.Linq;
using CaseStudy.WebApi.Models;

namespace CaseStudy.WebApi.Infrastructure
{
    /// <summary>
    /// Class for initialize DB.
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Seeds the database's values.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public static void SeedDatabase(DataContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (context.Products.Any()) return;
            for (int i = 0; i < 100; i++)
            {
                var img = $"http\\\\web.com\\{i}.png";
                context.Products.Add(new Product
                {
                    Name = $"{i} Product",
                    Price = 275 * i + 1,
                    Description = $"Description {i}",
                    ImgUri = new Uri(img, UriKind.RelativeOrAbsolute)
                });
            }
            context.SaveChanges();
        }
    }
}