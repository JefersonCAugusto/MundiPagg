using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DesafioMundi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DesafioMundi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AuthsController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        // POST api/<AuthsController>
        [HttpPost("NewAcount")]
        public async Task<ActionResult> RegistrarAsync([FromBody] Register register)
        {
            //valida o model state
            //cria o usuario do identity
            var user = new IdentityUser
            {
                UserName = register.Email,
                Email = register.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, register.ConfirmPassword);
            if (!result.Succeeded) return BadRequest(result.Errors);
            await _signInManager.SignInAsync(user, false);
            return Ok(await CreateJwt(register.Email));
        }

         
        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync([FromBody] Login login)
        {
            //valida o model state
            var result= await _signInManager
                .PasswordSignInAsync(login.Email, login.Password, false, true);
            if (result.Succeeded) return Ok(await CreateJwt(login.Email));
            return BadRequest("usuario ou senha inválidos");
        }

        private async Task<string> CreateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidIn,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpireHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        
        }
    }
}
