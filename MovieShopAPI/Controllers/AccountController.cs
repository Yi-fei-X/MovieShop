using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;

            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please check the data you entered");  
            }
            var newUser = await _userService.RegisterUser(requestModel);
            return Ok(newUser);
        }

        [HttpPost("login")]     // combine with HttpPost and Route
        public async Task<IActionResult> Login([FromBody] UserLoginRequestModel requestModel)     //FromBody read the information from body
        {
            var user = await _userService.LoginUser(requestModel);
            if (user == null)
            {
                // invalid un/password
                return Unauthorized();      //401
            }
            // un/pw is valid
            // create JWT and sent it to client(Angular), add claims info in the token.
            return Ok(new { token = GenerateJWT(user)});      //anonymous type with only 1 property token
        }


        private string GenerateJWT(UserLoginResponseModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email ),   
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),     // you can also use JwtRegisteredClaimNames
                new Claim("MyDesignation", "developer")     //Claim can be anything
                //new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToShortDateString()),
                //new Claim("FullName", user.FirstName + " " + user.LastName)
            };

            // add the required claims to the identity object
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaims(claims);

            //Microsoft.IdentityModel.Tokens is for the securityKey, credentials and expires 
            // get the secret key to sign the token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));     // Get the security key from appsetting

            // specify the algorithm to sign the token
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("ExpirationHours"));

            // creating the token using System.IdentityModel.Tokens.Jwt;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor       //specify what properties our token should have
            {
                Subject = claimsIdentity,
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = _configuration["Issuer"],
                Audience = _configuration["Audience"]
            };

            var encodedJwt = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(encodedJwt);     //change token to string

        }







    }
}
