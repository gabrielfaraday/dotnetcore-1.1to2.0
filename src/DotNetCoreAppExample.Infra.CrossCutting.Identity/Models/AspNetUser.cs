using DotNetCoreAppExample.Domain.Core.Interfaces;
using DotNetCoreAppExample.Infra.CrossCutting.Identity.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace DotNetCoreAppExample.Infra.CrossCutting.Identity.Models
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public IEnumerable<Claim> GetClaimsIdentity() => _accessor.HttpContext.User.Claims;

        public bool IsAuthenticated() => _accessor.HttpContext.User.Identity.IsAuthenticated;

        public Guid GetUserId() => IsAuthenticated()
            ? Guid.Parse(_accessor.HttpContext.User.GetUserId())
            : Guid.NewGuid();
    }
}