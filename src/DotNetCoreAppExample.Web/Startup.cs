using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DotNetCoreAppExample.Infra.CrossCutting.IoC;
using DotNetCoreAppExample.Infra.CrossCutting.Identity.Data;
using DotNetCoreAppExample.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreAppExample.Infra.CrossCutting.AspnetFilters;
using DotNetCoreAppExample.Infra.CrossCutting.LoggerProviders.ElasticSearch;
using DotNetCoreAppExample.Infra.CrossCutting.LoggerProviders;
using Microsoft.AspNetCore.Identity;

namespace DotNetCoreAppExample.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("PermiteVerContatos", policy => policy.RequireClaim("Contatos", "Ver"));
                options.AddPolicy("PermiteGerenciarContatos", policy => policy.RequireClaim("Contatos", "Gerenciar"));
                options.AddPolicy("PermiteGerenciarTelefones", policy => policy.RequireClaim("Contatos", "GerenciarTelefones"));
            });

            //services.AddAuthentication();

            services.AddMvc(options =>
            {
                options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalExceptionHandlingFilter)));
                options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));
            });

            services.AddAutoMapper();

            services.Configure<ESClientProviderConfig>(Configuration.GetSection("ElasticSearch"));

            DependencyInjectionBootStrapper.RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //PARA HABILITAR O LOG AUTOMATICO NO ELASTIC SEARCH DESCOMENTE A LINHA ABAIXO:
            //loggerFactory.AddElasticSearchLogger(app.ApplicationServices);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/erro-de-aplicacao");
                app.UseStatusCodePagesWithReExecute("/erro-de-aplicacao/{0}");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
