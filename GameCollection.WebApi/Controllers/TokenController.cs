using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GameCollection.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GameCollection.WebApi.Controllers
{
    [Produces("application/json")]
    public class TokenController : Controller
    {
        private IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Create Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Token/CreateToken")]
        [ProducesResponseType(typeof(OkObjectResult), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        [ProducesResponseType(typeof(void), 500)]
        public IActionResult CreateToken([FromBody] UserRegisterAuthenticate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            IActionResult response = Unauthorized();

            if (IsAuthenticate(model))
            {
                var token = GetJwtSecurityToken(model.Email);
                return Ok(new { token = token.token, validTo = token.validTo });
            }

            return response;
        }

        private (string token, string validTo) GetJwtSecurityToken(string email)
        {

            var claims = new[] {
                //new Claim(JwtRegisteredClaimNames.Sub, "Ludovic"),
                new Claim(JwtRegisteredClaimNames.Email, email),
                //new Claim(JwtRegisteredClaimNames.Birthdate, "1986-04-18"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: _config["Jwt:Issuer"], audience: _config["Jwt:Issuer"]
                , claims: claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: creds);

            return (new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo.ToLocalTime().ToString());
        }

        private bool IsAuthenticate(UserRegisterAuthenticate model)
        {
            if (model.Email == "lhu@softfluent.com" && model.Password == "azerty")
            {
                return true;
            }

            return false;
        }
    }
}