using AutoMapper;
using DotNetCoreAppExample.Infra.CrossCutting.Identity.Data;
using DotNetCoreAppExample.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DotNetCoreAppExample.Infra.CrossCutting.IoC;
using DotNetCoreAppExample.Services.Api.Configurations;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreAppExample.Infra.CrossCutting.AspnetFilters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using DotNetCoreAppExample.Infra.CrossCutting.Identity.Authorization;
using System;

namespace DotNetCoreAppExample.Services.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        //private const string SecretKey = "DotNetCoreAppExample@TokenSecretKey123";
        //private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Cookies.ApplicationCookie.AutomaticChallenge = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddOptions();
            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.UseCentralRoutePrefix(new RouteAttribute("api"));

                var policy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
                //options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("PermiteVerContatos", policy => policy.RequireClaim("Contatos", "Ver"));
                options.AddPolicy("PermiteGerenciarContatos", policy => policy.RequireClaim("Contatos", "Gerenciar"));
                options.AddPolicy("PermiteGerenciarTelefones", policy => policy.RequireClaim("Contatos", "GerenciarTelefones"));
            });

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtTokenOptions));
            var jwtAppSettingSecurity = Configuration.GetSection(nameof(JwtTokenSecurity));
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppSettingSecurity[nameof(JwtTokenSecurity.SecretKey)]));

            services.Configure<JwtTokenOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtTokenOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtTokenOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAutoMapper();

            DependencyInjectionBootStrapper.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtTokenOptions));
            var jwtAppSettingSecurity = Configuration.GetSection(nameof(JwtTokenSecurity));
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppSettingSecurity[nameof(JwtTokenSecurity.SecretKey)]));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtTokenOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtTokenOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero,
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc();
        }
    }
}
