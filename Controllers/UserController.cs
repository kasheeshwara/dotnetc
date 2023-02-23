using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceProject.Models;
using ServiceProject.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Identity;
using ServiceProject.Helper;

namespace ServiceProject.Controllers
{
    [Route("api/[controller]")]
   
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ServiceContext _authContext;
        public UserController(ServiceContext authContext)
        {
          
            _authContext = authContext;

        }
        
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = await _authContext.Users
                .FirstOrDefaultAsync(x => x.Name == userObj.Name);
            if (user == null)
                return NotFound(new { Message = "User Not Found" });

            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                return BadRequest(new { Message = "Password is Incorrect" });
            }


            user.Token = CreateJwt(user);
            var newAccessToken = user.Token;
            // var newRefreshToken = CreateRefreshToken();
            //user.RefreshToken = newRefreshToken;
            //user.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);
            await _authContext.SaveChangesAsync();

            return Ok(new TokenApiDto()
            {
                AccessToken = newAccessToken,
                // RefreshToken = newRefreshToken
            });

        }
        [HttpPost("verifyemail")]
        public async Task<IActionResult> VerifyEmail([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = await _authContext.Users
                .FirstOrDefaultAsync(x => x.Email == userObj.Email);
            if (user == null)
                return NotFound(new { Message = "User Not Found" });

            await _authContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "Email is Verified"
            });
        }

       /* [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();
            //check username
            if (await CheckUserNameExistAsync(userObj.Name))
                return BadRequest(new { Message = "Username Already Exist!" });
            //check Email
            if (await CheckEmailExistAsync(userObj.Email))
                return BadRequest(new { Message = "Email Already Exist!" });


            //check Password Strength
            var pass = CheckPasswordStrength(userObj.Password);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass.ToString() });

            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.RegisteredDate = DateTime.Now;
            //userObj.Role = "User";
            userObj.Token = "";
            await _authContext.Users.AddAsync(userObj);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "User Registered"
            });
        }

        private Task<bool> CheckUserNameExistAsync(string username)
             => _authContext.Users.AnyAsync(x => x.Name == username);


        private Task<bool> CheckEmailExistAsync(string email)
             => _authContext.Users.AnyAsync(x => x.Email == email);

        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
                sb.Append("Minimum password length should be 8" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]")
                && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password Should be Alphanumeric" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]")))
                sb.Append("Password should contain special Chars" + Environment.NewLine);
            return sb.ToString();
        } */
        private string CreateJwt(User user)
        {
            var JwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("VeryVerySecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, $"{user.Name}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = credentials,

            };
            var token = JwtTokenHandler.CreateToken(tokenDescriptor);
            return JwtTokenHandler.WriteToken(token);
        }



        private ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes("VeryVerySecret.....");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("This is invalid Token");
            return principal;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            return Ok(await _authContext.Users.ToListAsync());
        }


    }

}
