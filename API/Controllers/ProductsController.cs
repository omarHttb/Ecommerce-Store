using Core.entites;
using Core.Interfaces;
using Infastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;




[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repository) : ControllerBase
{
  

 [HttpGet]
 public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand,string? type,
  string? sort )
 {

    return Ok(await repository.GetProductsAsync(brand,type,sort));
 }

 [HttpGet("{id:int}")]
 public async Task<ActionResult<Product>> GetProduct(int id)
 {
        var product = await repository.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
 }

[HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repository.AddProduct(product);
        if(!await repository.SaveChangesAsync())
        {
            return BadRequest("failed to add product");
        }

        return product;

    }
[HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !repository.ProductExists(id) )
        {
            return BadRequest();
        }

        repository.UpdateProduct(product);
      if(!await repository.SaveChangesAsync())
        {
            return BadRequest("failed to Update product");
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task <ActionResult<Product>> DeleteProduct(int id)
    {
        var product = await repository.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        repository.DeleteProduct(product);

      if(!await repository.SaveChangesAsync())
        {
            return BadRequest("failed to Update product");
        }

        return NoContent();
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetProductBrands()
    {
        return Ok(await repository.GetBrandsAsync());
    }

        [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypesAsync()
    {
        return Ok(await repository.GetTypesAsync());
    }

}