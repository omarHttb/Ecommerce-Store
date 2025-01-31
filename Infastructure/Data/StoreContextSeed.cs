

using System.Text.Json;
using Core.entites;

namespace Infastructure.Data;

public class StoreContextSeed
{

    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.Products.Any())
        {
            var productsData = File.ReadAllText("../Infastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (products == null) return;
            foreach (var product in products)
            {
                context.Products.AddRange(product);
            }

            await context.SaveChangesAsync();
        }
    }

}
