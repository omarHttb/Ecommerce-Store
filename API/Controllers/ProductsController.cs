using Core.entites;
using Core.Interfaces;
using Core.Specification;
using Infastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;




[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericRepository<Product> repository) : ControllerBase
{
  

 [HttpGet]
 public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand,string? type,
  string? sort )
 {
    var spec = new ProductSpecification(brand,type,sort);

    var products = await repository.ListAsync(spec);

    return Ok(products);
 }

 [HttpGet("{id:int}")]
 public async Task<ActionResult<Product>> GetProduct(int id)
 {
        var product = await repository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
 }

[HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repository.Add(product);
        if(!await repository.SaveAllAsync())
        {
            return BadRequest("failed to add product");
        }

        return product;

    }
[HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !repository.Exists(id) )
        {
            return BadRequest();
        }

        repository.Update(product);
      if(!await repository.SaveAllAsync())
        {
            return BadRequest("failed to Update product");
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task <ActionResult<Product>> DeleteProduct(int id)
    {
        var product = await repository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        repository.Remove(product);

      if(!await repository.SaveAllAsync())
        {
            return BadRequest("failed to Update product");
        }

        return NoContent();
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetProductBrands()
    {
        var spec = new BrandListSpecification();

        return Ok(await repository.ListAsync(spec));
    }

        [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypesAsync()
    {
            var spec = new TypeListSpecification();

        return Ok(await repository.ListAsync(spec));
    }

}