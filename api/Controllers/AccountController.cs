using api.Configuration;
using api.Models;
using Api.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/api")]
    public class AccountController : ControllerBase
    {        
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        public AccountController(UserManager<IdentityUser> userManager, IConfiguration config)
        {                     
           _userManager = userManager;
            _config = config; 
        }
       
        [HttpPost("account/register")]
        public async Task<ActionResult> Register([FromBody]UserRegistrationRequestDto user)
        {           
            if (ModelState.IsValid)
            {
                var username= await _userManager.FindByNameAsync(user.name);
                var email= await _userManager.FindByEmailAsync(user.email);            
                // SE o Email ou o username ja existe ou seja for diferente de nulo 
                // então vai exibir um erro
                
                if (email != null | username != null)
                {
                    return BadRequest(new AuthResult
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            $"Email:{email} or username:{username} Already exist"
                        }
                    });
                }

                //se não, cria um novo usuario
                var new_user = new IdentityUser()
                {
                    UserName = user.name,
                    Email = user.email,                   
                };

                // finalizando a criação cadastrando uma senha para o usuario
                var is_created = await _userManager.CreateAsync(new_user, user.password);

                // verificando se a criação do usuario ocorreu com sucesso
                // se sim, então o token será gerado.
                if (is_created.Succeeded)
                {
                    //generated token
                    var token = GeneratedToken(new_user);
                    
                    return Ok(new AuthResult()
                    {
                        Result = true,
                        Token = token
                    });
                }
                // se não retorna um Badrequest 
                return BadRequest(new AuthResult
                {
                    Errors = new List<string>()
                    {
                        "erro in the server"
                    },
                    Result = false,
                }); 
            }
            return BadRequest();
        }
        [HttpPost("auth/login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto login)
        {
            var doLogin = await _userManager.FindByEmailAsync(login.email);
            if (doLogin == null)
            {
                BadRequest(new AuthResult(){
                    Result = false,
                    Errors= new List<string>()
                    {
                        "Error internal in the server"
                    }
                });
            }
           var ChechPassTrue = await _userManager.CheckPasswordAsync(doLogin, login.password);
            if (!ChechPassTrue)
            {
                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "invalid credentials"
                    }
                });
            }
             var jwttoken = GeneratedToken(doLogin);
            return Ok( new AuthResult()
            {
               Result = true,
               Token  = jwttoken
            });    
        }
        private string GeneratedToken(IdentityUser user)
        {       
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config.GetSection("JWTConfig:Secret").Value);

            //abaixo a config de descriptaçao do token
            var JwtDescriptor = new SecurityTokenDescriptor()
            {
                //configuração do header
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),
              //de expiração da requisição
                Expires= DateTime.Now.AddHours(8),
                SigningCredentials = new SigningCredentials(new 
                                    SymmetricSecurityKey(key),
                                    SecurityAlgorithms.HmacSha256)
            };
            // a criação do token
            var token = jwtTokenHandler.CreateToken(JwtDescriptor);
            var jwt = jwtTokenHandler.WriteToken(token);
            return jwt;
        }       
    }
}
