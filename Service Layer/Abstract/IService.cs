

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Service_Layer.Abstract
{
    // single interface to all 'repositories'

    public interface IService<TEntity> where TEntity: class 
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        // This method was not in the videos, but I thought it would be useful to add.
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        TEntity Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

    }
}
