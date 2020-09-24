using CaseStudy.WebApi.Models;
using Serilog;
using System;
using System.Linq;

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

            if (context.Products.Any())
            {
                return;
            }
            Log.Debug("start seed data");
            for (int i = 0; i < 100; i++)
            {
                context.Products.Add(new Product
                {
                    Name = $"{i} Product",
                    Price = 275 * i + 1,
                    Description = $"Description {i}",
                    ImgUri = new Uri($"http\\\\web.com\\{i}.png", UriKind.RelativeOrAbsolute)
                });
            }
            context.SaveChanges();
            Log.Debug("finish seed data");
        }

        /// <summary>
        /// Seed the database's value - 10000 items.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public static void SeedDatabaseLarge(DataContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (context.Products.Any()) return;
            Log.Debug("start large seed data");
            for (int i = 0; i < 10000; i++)
            {
                context.Products.Add(new Product
                {
                    Name = $"{i} Product",
                    Price = 275 + i,
                    Description = $"Description {i}",
                    ImgUri = new Uri($"http\\\\web.com\\{i}.png", UriKind.RelativeOrAbsolute)
                });
            }
            context.SaveChanges();
            Log.Debug("finish large seed data");
        }
    }
}