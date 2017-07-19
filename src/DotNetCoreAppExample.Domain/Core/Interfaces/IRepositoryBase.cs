using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DotNetCoreAppExample.Domain.Core.Interfaces
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : EntityBase<TEntity>
    {
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity FindById(Guid id);
        IEnumerable<TEntity> GetAll();
        void Delete(Guid id);
        TEntity Update(TEntity entity);
        int SaveChanges();
    }
}