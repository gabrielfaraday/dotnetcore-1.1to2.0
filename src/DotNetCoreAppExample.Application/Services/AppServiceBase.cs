using AutoMapper;
using DotNetCoreAppExample.Application.Interfaces;
using DotNetCoreAppExample.Domain.Core;
using DotNetCoreAppExample.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace DotNetCoreAppExample.Application.Services
{
    public abstract class AppServiceBase<TEntity, TEntityViewModel, TService> : IAppServiceBase<TEntityViewModel> where TEntity : EntityBase<TEntity> where TEntityViewModel : class where TService : IServiceBase<TEntity>
    {
        protected readonly IUnitOfWork _uow;
        protected readonly TService _service;
        protected readonly IMapper _mapper;

        public AppServiceBase(IUnitOfWork uow, TService service, IMapper mapper)
        {
            _uow = uow;
            _service = service;
            _mapper = mapper;
        }

        public virtual TEntityViewModel Add(TEntityViewModel entityViewModel)
        {
            var entity = _service.Add(_mapper.Map<TEntity>(entityViewModel));

            if (entity.ValidationResult.IsValid)
                Commit();

            return _mapper.Map<TEntityViewModel>(entity);
        }

        public virtual TEntityViewModel FindById(Guid id)
        {
            return _mapper.Map<TEntityViewModel>(_service.FindById(id));
        }

        public virtual IEnumerable<TEntityViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<TEntityViewModel>>(_service.GetAll());
        }

        public virtual TEntityViewModel Update(TEntityViewModel entityViewModel)
        {
            var entity = _service.Update(_mapper.Map<TEntity>(entityViewModel));

            if (entity.ValidationResult.IsValid)
                Commit();

            return _mapper.Map<TEntityViewModel>(entity);
        }

        public virtual void Delete(Guid id)
        {
            _service.Delete(id);
            Commit();
        }

        protected void Commit()
        {
            _uow.Commit();
        }

        public virtual void Dispose()
        {
            _service.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
