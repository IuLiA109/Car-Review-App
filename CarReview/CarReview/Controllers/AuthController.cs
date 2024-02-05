using CarReview.Dto;
using CarReview.Interfaces;
using CarReview.Models;
using CarReview.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CarReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IUserBigRepository _userRepository;
        private readonly IUserDetailsService _userDetailsService;

        public AuthController(IConfiguration configuration, IUserBigRepository userRepository, IUserDetailsService userDetailsService)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _userDetailsService = userDetailsService;
        }

        [HttpPost("{role}/register/{language}")]
        public async Task<ActionResult<User>> Register(UserDto request, string role, string language)
        {
            var userr = _userRepository.GetUsers().Where(u => u.UserName.Trim() == request.UserName.Trim()).FirstOrDefault();

            if (userr != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }
            //var user = new User();

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User() 
            {
                UserName = request.UserName,
                Id = request.Id,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Role.User
            };
            
            if (role.ToLower() == "admin")
            {
                Role userRole = Role.Admin;
                user.Role = userRole;
            }
            else
            {
                Role userRole = Role.User;
                user.Role = userRole;
            }

            if (!_userRepository.CreateUser(user))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            var userDetails = new UserDetails()
            {
                User = user,
                UserId = user.Id,
                Language = language,
            };

            if (!_userDetailsService.CreateUserDetails(userDetails))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserLoginDto request)
        {
            if(!_userRepository.UserExistsByUsername(request.UserName)) 
            {
                return BadRequest("User not found");
            }
            var userr = _userRepository.GetUserByUsername(request.UserName);

            if (!VerifyPasswordHash(request.Password, userr.PasswordHash, userr.PasswordSalt))
            {
                return BadRequest("Wrong password!");
            }

            string token = CreateToken(userr);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };
            if (user.Role == Role.Admin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims : claims,
                expires : DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) 
        {
            using (var hmac = new HMACSHA512()) 
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash= hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }

        }


    }
}
