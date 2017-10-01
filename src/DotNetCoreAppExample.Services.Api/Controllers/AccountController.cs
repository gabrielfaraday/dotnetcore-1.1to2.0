using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DotNetCoreAppExample.Infra.CrossCutting.Identity.Models;
using Microsoft.Extensions.Logging;
using DotNetCoreAppExample.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DotNetCoreAppExample.Infra.CrossCutting.Identity.Models.AccountViewModels;
using DotNetCoreAppExample.Application.ViewModels;
using DotNetCoreAppExample.Infra.CrossCutting.Identity.Authorization;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace DotNetCoreAppExample.Services.Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IUsuarioDadosAppService _usuarioDadosAppService;
        private readonly JwtTokenOptions _jwtTokenOptions;

        public AccountController(
                    UserManager<ApplicationUser> userManager,
                    SignInManager<ApplicationUser> signInManager,
                    ILoggerFactory loggerFactory,
                    IOptions<JwtTokenOptions> jwtTokenOptions,
                    IUsuarioDadosAppService usuarioDadosAppService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _usuarioDadosAppService = usuarioDadosAppService;
            _jwtTokenOptions = jwtTokenOptions.Value;

            ThrowIfInvalidOptions(_jwtTokenOptions);
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        [HttpPost]
        [AllowAnonymous]
        [Route("registrar")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return Response();

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            user.Claims.Add(new IdentityUserClaim<string> { ClaimType = "Contatos", ClaimValue = "Ver" });
            user.Claims.Add(new IdentityUserClaim<string> { ClaimType = "Contatos", ClaimValue = "Gerenciar" });
            user.Claims.Add(new IdentityUserClaim<string> { ClaimType = "Contatos", ClaimValue = "GerenciarTelefones" });

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var usuarioDados = new UsuarioDadosViewModel
                {
                    Id = Guid.Parse(user.Id),
                    Nome = model.Nome,
                    CPF = model.CPF
                };

                var retorno = _usuarioDadosAppService.Add(usuarioDados);

                if (!retorno.ValidationResult.IsValid)
                {
                    await _userManager.DeleteAsync(user);
                    return Response(viewModel: retorno, result: null);
                }

                _logger.LogInformation(1, "Usuario criado com sucesso!");

                var response = GerarTokenUsuario(new LoginViewModel { Email = model.Email, Password = model.Password });
                return Response(viewModel: null, result: response);
            }

            AdicionarErrosIdentity(result);
            return Response();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return Response();

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            if (result.Succeeded)
            {
                _logger.LogInformation(1, "Usuario logado com sucesso");

                var response = GerarTokenUsuario(model);
                return Response(viewModel: null, result: response);
            }

            ModelState.AddModelError(result.ToString(), "Falha ao realizar o login");
            return Response();
        }

        private async Task<object> GerarTokenUsuario(LoginViewModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            var userClaims = await _userManager.GetClaimsAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, await _jwtTokenOptions.JtiGenerator()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtTokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64));

            var jwt = new JwtSecurityToken(
                  issuer: _jwtTokenOptions.Issuer,
                  audience: _jwtTokenOptions.Audience,
                  claims: userClaims,
                  notBefore: _jwtTokenOptions.NotBefore,
                  expires: _jwtTokenOptions.Expiration,
                  signingCredentials: _jwtTokenOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var orgUser = _usuarioDadosAppService.FindById(Guid.Parse(user.Id));

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_jwtTokenOptions.ValidFor.TotalSeconds,
                user = new
                {
                    id = user.Id,
                    nome = orgUser.Nome,
                    email = user.Email,
                    cpf = orgUser.CPF,
                    claims = userClaims.Select(c => new { c.Type, c.Value })
                }
            };

            return response;
        }

        private static void ThrowIfInvalidOptions(JwtTokenOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            else if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtTokenOptions.ValidFor));
            else if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(JwtTokenOptions.SigningCredentials));
            else if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(JwtTokenOptions.JtiGenerator));
        }
    }
}