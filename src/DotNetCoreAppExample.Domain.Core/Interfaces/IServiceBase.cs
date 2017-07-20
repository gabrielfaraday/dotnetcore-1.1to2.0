using System;
using System.Collections.Generic;

namespace DotNetCoreAppExample.Domain.Core.Interfaces
{
    public interface IServiceBase<TEntity> : IDisposable where TEntity : EntityBase<TEntity>
    {
        TEntity Add(TEntity entity);
        TEntity FindById(Guid id);
        IEnumerable<TEntity> GetAll();
        TEntity Update(TEntity entity);
        void Delete(Guid id);
    }
}
