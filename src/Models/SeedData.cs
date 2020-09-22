using System;
using System.Linq;

namespace CaseStudy.WebApi.Models
{
    public static class SeedData
    {

        public static void SeedDatabase(DataContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
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