using System;

namespace DotNetCoreAppExample.Domain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
