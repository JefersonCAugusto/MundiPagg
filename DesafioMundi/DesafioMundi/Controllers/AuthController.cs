using DesafioMundi.Entities;
using DesafioMundi.Entities.AuthIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

 
namespace DesafioMundi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AuthController(SignInManager<IdentityUser> signInManager
                                , UserManager<IdentityUser> userManager
                                , IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }


        /// <summary>
        /// Cria um movo usuário
        /// </summary>
        /// <remarks>
        ///     {
        ///         "Email":"jeferson@gmail.com.br",
        ///         "Password":"1234abC@",
        ///         "ConfirmPassword":"1234abC@" 
        ///     }
        /// </remarks>
        /// <param name="register">Objeto contendo os dados do novo usuário</param>
        /// <returns>Retonr um token de Bearer com validade de uma hora </returns>
        
        [HttpPost("NewAcount")]
        public async Task<ActionResult> RegisterUser([FromBody] Register register)
        {
            var user = new IdentityUser
            {
                UserName = register.Email,
                Email = register.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded) 
                return BadRequest(result.Errors);
            
            await _signInManager.SignInAsync(user, false);
            return Ok(await CreateJwt(register.Email));
        }

        /// <summary>
        /// Faz login na API após expiração do Token
        /// </summary>
        /// <remarks>
        ///		{
        ///   	 	"Email":"jefero@gmail.com.br",
        ///			"Password":"1234abC@" 
        ///		}	
        /// </remarks>
        /// <param name="login">Objeto informado no body contendo usuário e senha</param>
        /// <returns></returns>


        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync([FromBody] Login login)
        { 
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
