using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Core.entites;

namespace Core.Specification;

public class ProductSpecification : BaseSpecification<Product>
{

    public ProductSpecification(string? brand , string? type,string? sort) : 
    base(x => (string.IsNullOrEmpty(brand) || x.Brand == brand) && 
    (string.IsNullOrEmpty(type) || x.Type == type))
    {
        switch (sort)
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
