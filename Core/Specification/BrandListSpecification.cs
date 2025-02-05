using System;
using Core.entites;

namespace Core.Specification;

public class BrandListSpecification : BaseSpecification<Product,string>
{

    public BrandListSpecification()
    {
        AddSelect(x => x.Brand);
        ApplyDistinct();
    }

}
