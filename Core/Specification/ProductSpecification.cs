using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Core.entites;
using Core.Specification;

namespace BuisinessLogic.Specification;

public class ProductSpecification : BaseSpecification<Product>
{

    public ProductSpecification(ProductSpecParams specParams) : 
    base(x => 
    (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
    (specParams.Brands.Count == 0 || specParams.Brands.Contains(x.Brand)) && 
    (specParams.types.Count == 0 ||  specParams.types.Contains(x.Type)))
    {

        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        switch (specParams.Sort)
        {
            case "priceAsc":
                AddOrderBy(p => p.Price);
                break;
            case "priceDesc":
                AddOrderByDesc(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name);
                break;
        }
    }

}
