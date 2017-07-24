using AutoMapper;
using DotNetCoreAppExample.Application.Interfaces;
using DotNetCoreAppExample.Application.Services;
using DotNetCoreAppExample.Domain.Contatos.Interfaces;
using DotNetCoreAppExample.Domain.Contatos.Services;
using DotNetCoreAppExample.Domain.Core.Interfaces;
using DotNetCoreAppExample.Infra.CrossCutting.AspnetFilters;
using DotNetCoreAppExample.Infra.CrossCutting.Identity.Services;
using DotNetCoreAppExample.Infra.Data;
using DotNetCoreAppExample.Infra.Data.Context;
using DotNetCoreAppExample.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetCoreAppExample.Infra.CrossCutting.IoC
{
    public class DependencyInjectionBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //ASPNET
            

            //Application
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            services.AddScoped<IContatoAppService, ContatoAppService>();

            //Domain
            services.AddScoped<IContatoService, ContatoService>();

            //Infra.Data
            services.AddScoped<IContatoRepository, ContatoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<MainContext>();

            //Infra.Identity
            //services.AddScoped<IUser, AspNetUser>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            //Infra.Filtros
            services.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
            services.AddScoped<ILogger<GlobalActionLogger>, Logger<GlobalActionLogger>>();
            services.AddScoped<GlobalExceptionHandlingFilter>();
            services.AddScoped<GlobalActionLogger>();
        }
    }
}
