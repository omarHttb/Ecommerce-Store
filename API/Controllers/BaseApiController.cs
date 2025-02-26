using System;
using API.RequestHelpers;
using Core.entites;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{

    protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repository, 
    ISpecification<T> spec, int pageIndex, int pageSize) where T : BaseEntity
    {
        var products = await repository.ListAsync(spec);

        var count = await repository.CountAsync(spec);

        var Pagination = new Pagination<T>(pageIndex, pageSize, count, products);

        return Ok(Pagination);
    }
}
