using System;
using System.Collections.Generic;

namespace DotNetCoreAppExample.Application.Interfaces
{
    public interface IAppServiceBase<TEntityViewModel> : IDisposable where TEntityViewModel : class
    {
        TEntityViewModel Add(TEntityViewModel entityViewModel);
        void Delete(Guid id);
        TEntityViewModel FindById(Guid id);
        IEnumerable<TEntityViewModel> GetAll();
        TEntityViewModel Update(TEntityViewModel entityViewModel);
    }
}
