using System;
using Core.entites;
using Core.Interfaces;

namespace Infastructure.Data;

public class SpecificationEvaluator<T>where T: BaseEntity
{

    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> specification)
    {
        if(specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        if(specification.OrderBy != null )
        {
            query = query.OrderBy(specification.OrderBy);
        }

        
        if(specification.OrderByDesc != null )
        {
            query = query.OrderByDescending(specification.OrderByDesc);
        }

        if(specification.isDistinct)
        {
            query = query.Distinct();
        }

              if(specification.isPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }


        return query;

    }

        public static IQueryable<TResult> GetQuery<Tspec,TResult>(IQueryable<T> query, ISpecification<T,TResult> specification)
    {
        if(specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        if(specification.OrderBy != null )
        {
            query = query.OrderBy(specification.OrderBy);
        }

        
        if(specification.OrderByDesc != null )
        {
            query = query.OrderByDescending(specification.OrderByDesc);
        }

        var selectQuery = query as IQueryable<TResult>;

        if(specification.Select != null)
        {
            selectQuery = query.Select(specification.Select);
        }
        

        if(specification.isDistinct)
        {
            selectQuery = selectQuery?.Distinct();
        }

        return selectQuery  ?? query.Cast<TResult>();

    }

}
