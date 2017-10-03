using AutoMapper;
using DotNetCoreAppExample.Infra.CrossCutting.Identity.Data;
using DotNetCoreAppExample.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DotNetCoreAppExample.Infra.CrossCutting.IoC;
using DotNetCoreAppExample.Services.Api.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DotNetCoreAppExample.Infra.CrossCutting.Identity.Authorization;
using System;
using DotNetCoreAppExample.Infra.CrossCutting.LoggerProviders.ElasticSearch;
using DotNetCoreAppExample.Infra.CrossCutting.LoggerProviders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace DotNetCoreAppExample.Services.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtTokenOptions));
            var jwtAppSettingSecurity = Configuration.GetSection(nameof(JwtTokenSecurity));
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppSettingSecurity[nameof(JwtTokenSecurity.SecretKey)]));

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(bearerOptions =>
                {
                    var paramsValidation = bearerOptions.TokenValidationParameters;

                    paramsValidation.ValidateIssuer = true;
                    paramsValidation.ValidIssuer = jwtAppSettingOptions[nameof(JwtTokenOptions.Issuer)];

                    paramsValidation.ValidateAudience = true;
                    paramsValidation.ValidAudience = jwtAppSettingOptions[nameof(JwtTokenOptions.Audience)];

                    paramsValidation.ValidateIssuerSigningKey = true;
                    paramsValidation.IssuerSigningKey = signingKey;

                    paramsValidation.RequireExpirationTime = true;
                    paramsValidation.ValidateLifetime = true;

                    paramsValidation.ClockSkew = TimeSpan.Zero;
                });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser()
                    .Build());

                auth.AddPolicy("PermiteVerContatos", policy => policy.RequireClaim("Contatos", "Ver"));
                auth.AddPolicy("PermiteGerenciarContatos", policy => policy.RequireClaim("Contatos", "Gerenciar"));
                auth.AddPolicy("PermiteGerenciarTelefones", policy => policy.RequireClaim("Contatos", "GerenciarTelefones"));
            });

            services.Configure<JwtTokenOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtTokenOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtTokenOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddOptions();
            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.UseCentralRoutePrefix(new RouteAttribute("api"));
                
                //options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));
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

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
