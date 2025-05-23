using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T,bool>>? Criteria {get;}    

    Expression<Func<T,object>>? OrderBy {get;}
    Expression<Func<T,object>>? OrderByDesc {get;}

    bool isDistinct {get;}

        int Take {get;}
    int Skip {get;}

    bool isPagingEnabled {get;}
    IQueryable<T> ApplyCriteria(IQueryable<T> query);


}

public interface ISpecification<T,Tresult> : ISpecification<T>
{
    Expression<Func<T,Tresult>>? Select {get;}
}
