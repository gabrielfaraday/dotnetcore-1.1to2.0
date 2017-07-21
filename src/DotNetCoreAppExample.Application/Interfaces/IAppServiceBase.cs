using System;
using System.Collections.Generic;

namespace DotNetCoreAppExample.Application.Interfaces
{
    public interface IAppServiceBase<TEntityViewModel, TNewEntityViewModel> : IDisposable where TEntityViewModel : class where TNewEntityViewModel : TEntityViewModel
    {
        TNewEntityViewModel Add(TNewEntityViewModel entityViewModel);
        void Delete(Guid id);
        TEntityViewModel FindById(Guid id);
        IEnumerable<TEntityViewModel> GetAll();
        TEntityViewModel Update(TEntityViewModel entityViewModel);
    }
}
