using System;
using System.Dynamic;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specification;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{



    protected BaseSpecification() : this(null){}

    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy  { get; private set;}

    public Expression<Func<T, object>>? OrderByDesc { get; private set;}

    public bool isDistinct {get ; private set;}

    public int Take {get ; private set;}

    public int Skip {get ; private set;}

    public bool isPagingEnabled {get ; private set;}

public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if(criteria != null)
        {
            query = query.Where(criteria);
        }
        return query;
    }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

        protected void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDesc = orderByDescExpression;
    }

    protected void ApplyDistinct()
    {
        isDistinct = true;
    }

        protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        isPagingEnabled = true;
    }
}


public class BaseSpecification<T,Tresult>(Expression<Func<T, bool>>? criteria) 
: BaseSpecification<T>(criteria), ISpecification<T,Tresult>
{

    protected BaseSpecification() : this(null){}

    public Expression<Func<T, Tresult>>? Select { get; private set; }

    protected void AddSelect(Expression<Func<T, Tresult>> selectExpression)
    {
        Select = selectExpression;
    }
}