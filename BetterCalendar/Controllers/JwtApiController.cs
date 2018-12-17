using BetterCalendar.Data;
using BetterCalendar.Models.Api;
using BetterCalendar.services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BetterCalendar.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class JwtApiController : Controller
    {
        private IConfiguration _configuration;
        private ApplicationDbContext _context;
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private TokenManager _tokenManager;
        private EventsService _eventsService;

        public JwtApiController(IConfiguration configuration,
            ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            TokenManager tokenManager
            )
        {
            _configuration = configuration;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenManager = tokenManager;
        }

        #region login/logout

        [AllowAnonymous]
        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> LogInAsync([FromBody] LoginViewModel model)
        {
            var errorMessage = "Invalid username or password";


            // check if user provided username or email
            if (model?.UsernameOrEmail == null || string.IsNullOrEmpty(model.UsernameOrEmail))
                return BadRequest(error: errorMessage);

            //check if it is email            
            var isEmail = model.UsernameOrEmail.Contains("@");

            // Find user by emial or name
            var user = isEmail ?
                await _userManager.FindByEmailAsync(model.UsernameOrEmail) :
                await _userManager.FindByNameAsync(model.UsernameOrEmail);

            if (user == null) return NotFound();

            // check password
            var isValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);


            if (!isValidPassword) return BadRequest(error: errorMessage);

            // if we get here user passed correct login details
            // TODO: send some user profile info and  token
            return Ok(new {
                UserName = user.UserName,
                Email = user.Email,
                Token = GenerateJwtToken(user)
            });
        }


        [Route("api/logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            _tokenManager.CancelToken();
            return NoContent();
        }


        #endregion

        #region API
        [Route("api/events/get-all/{userId}")]
        public IActionResult GetAll(string userId)
        {
            var result = _eventsService.GetAll(userId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        #endregion






        #region routes for testing

        [Route("api/private")]
        [HttpGet]
        public IActionResult Private()
        {
            return Ok(new
            {
                message = "this is private",
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/public")]
        public IActionResult Public()
        { 
            return Ok(new
            {
                message = "this is public",
            });
        }

        #endregion


        #region helpers

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
                // Unique ID for this token
                
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),

                // The username using the Identity name so it fills out the HttpContext.User.Identity.Name value
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),

                // Add user Id so that UserManager.GetUserAsync can find the user based on Id
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var credentails = new SigningCredentials(

                // secretkey from config
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
                "HS256"
                );


            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: credentails,
                expires: DateTime.Now.AddDays(1)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

    }
}
