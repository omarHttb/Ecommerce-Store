using BuisinessLogic.Specification;
using Core.entites;
using Core.Interfaces;
using Core.Specification;
using Infastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;




[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseApiController
{
  
  private readonly IGenericRepository<Product> _repository;

    public ProductsController(IGenericRepository<Product> repository)
    {
        _repository = repository;
    }

 [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);
            return await CreatePagedResult(_repository, spec, specParams.PageIndex, specParams.PageSize);
        }


 [HttpGet("{id:int}")]
 public async Task<ActionResult<Product>> GetProduct(int id)
 {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
 }

[HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _repository.Add(product);
        if(!await _repository.SaveAllAsync())
        {
            return BadRequest("failed to add product");
        }

        return product;

    }
[HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !_repository.Exists(id) )
        {
            return BadRequest();
        }

        _repository.Update(product);
      if(!await _repository.SaveAllAsync())
        {
            return BadRequest("failed to Update product");
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task <ActionResult<Product>> DeleteProduct(int id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        _repository.Remove(product);

      if(!await _repository.SaveAllAsync())
        {
            return BadRequest("failed to Update product");
        }

        return NoContent();
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetProductBrands()
    {
        var spec = new BrandListSpecification();

        return Ok(await _repository.ListAsync(spec));
    }

        [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypesAsync()
    {
            var spec = new TypeListSpecification();

        return Ok(await _repository.ListAsync(spec));
    }

}