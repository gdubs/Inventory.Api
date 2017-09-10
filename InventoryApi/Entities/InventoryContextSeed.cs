using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Entities
{
    public static class InventoryContextSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new InventoryContext(serviceProvider.GetRequiredService<DbContextOptions<InventoryContext>>()))
            {
                if (context.Products.Any())
                {
                    return;
                }

                context.Shelves.AddRange(
                    new Shelf { Name = "Snack Aisle" },
                    new Shelf { Name = "Drink Aisle" },
                    new Shelf { Name = "Meat Aisle" },
                    new Shelf { Name = "Hygiene Aisle" }
                );
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Chips",
                        ShelfId = 1
                    },
                    new Product
                    {
                        Name = "Soda",
                        ShelfId = 2
                    },
                    new Product
                    {
                        Name = "Wine",
                        ShelfId = 2
                    },
                    new Product
                    {
                        Name = "Meat",
                        ShelfId = 3
                    },
                    new Product
                    {
                        Name = "Toiletries",
                        ShelfId = 4
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
