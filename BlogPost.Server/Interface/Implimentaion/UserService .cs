//using BlogPost.Server.Interface.Interface;
//using BlogPost.Server.Model.Domain;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using System;
//using Microsoft.EntityFrameworkCore;
//using BlogPost.Server.Database;





//namespace BlogPost.Server.Interface.Implimentaion
//{
//    public class UserService : IUserService
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly IConfiguration _configuration;

//        public UserService(ApplicationDbContext context, IConfiguration configuration)
//        {
//            _context = context;
//            _configuration = configuration;
//        }

//        public async Task<string> RegisterAsync(RegisterModel model)
//        {
//            if (await _context.UserProfiles.AnyAsync(u => u.Email == model.Email))
//                return "Username already exists";

//            var user = new UserProfile
//            {


//                FirstName=model.FirstName,

//                LastName= model.LastName,
//                Email= model.Email,

//                Mobile=model.Mobile,
//                Gender= model.Gender,

//                Pwd= BCrypt.Net.BCrypt.HashPassword(model.Pwd)


//            };

//            _context.UserProfiles.Add(user);
//            await _context.SaveChangesAsync();
//            return "User registered successfully!";
//        }

//        public async Task<string> LoginAsync(UserLogin model)
//        {
//            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
//            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Pwd, user.Pwd))
//                return "Invalid username or password";

//            var token = GenerateJwtToken(user);
//            return token;
//        }

//        private string GenerateJwtToken(User user)
//        {
//            var claims = new[]
//            {
//            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
//            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//        };

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//            var token = new JwtSecurityToken(
//                issuer: _configuration["JwtSettings:Issuer"],
//                audience: _configuration["JwtSettings:Audience"],
//                claims: claims,
//                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:ExpirationInMinutes"])),
//                signingCredentials: creds);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }




//    }
//}











using BlogPost.Server.Interface.Interface;
using BlogPost.Server.Model.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using BlogPost.Server.Database;





namespace BlogPost.Server.Interface.Implimentaion
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(RegisterModel model)
        {
            if (await _context.UserProfiles.AnyAsync(u => u.Email == model.Email))
                return "Username already exists";

            var user = new UserProfile
            {


                FirstName=model.FirstName,

                LastName= model.LastName,
                Email= model.Email,

                Mobile=model.Mobile,
                Gender= model.Gender,

                Pwd= BCrypt.Net.BCrypt.HashPassword(model.Pwd)


            };

            _context.UserProfiles.Add(user);
            await _context.SaveChangesAsync();
            return "User registered successfully!";
        }

        public async Task<string> LoginAsync(UserLogin model)
        {
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (userProfile == null || !BCrypt.Net.BCrypt.Verify(model.Pwd, userProfile.Pwd))
                return "Invalid username or password";

            var token = GenerateJwtToken(userProfile);
            return token;
        }

        private string GenerateJwtToken(UserProfile userProfile)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userProfile.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:ExpirationInMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}











































































































