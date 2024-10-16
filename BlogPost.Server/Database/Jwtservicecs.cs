//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace BlogPost.Server.Database
//{
//    public class Jwtservicecs
//    {
//        public string SecretKey { get; set; }
//        public int TokenDuration { get; set; }
//        private readonly IConfiguration config;


//        public Jwtservicecs(IConfiguration _config)
//        {
//            config = _config;
//            this.SecretKey = config.GetSection("jwtConfig").GetSection("Key").Value;
//            this.TokenDuration = Int32.Parse(config.GetSection("jwtConfig").GetSection("Duration").Value);
//        }

//        public string GenerateToken(string id, string firstname, string lastname, string email, string mobile, string gender)
//        {
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));

//            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var payload = new[]
//            {
//    new Claim("UserID", id),
//    new Claim("FirstName", firstname),
//    new Claim("LastName", lastname),
//    new Claim("Email", email),
//    new Claim("Mobile", mobile),
//    new Claim("Gender", gender)
//};
//            var jwtToken = new JwtSecurityToken(
//                   issuer: "localhost",
//                   audience: "localhost",
//                   claims: payload,
//                   expires: DateTime.UtcNow.AddMinutes(TokenDuration),
//                   signingCredentials: signature
//               );

//            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
//        }
//    }
//}











using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogPost.Server.Database
{
    public class Jwtservicecs
    {
        private readonly IConfiguration _config;
        public string SecretKey { get; }
        public int TokenDuration { get; }

        public Jwtservicecs(IConfiguration config)
        {
            _config = config;
            this.SecretKey = _config.GetSection("JwtConfig").GetSection("Key").Value;

            // Ensure the key is at least 16 characters long
            if (string.IsNullOrEmpty(SecretKey) || SecretKey.Length < 16)
            {
                throw new ArgumentException("JWT SecretKey must be at least 16 characters long.");
            }

            this.TokenDuration = Int32.Parse(_config.GetSection("JwtConfig").GetSection("Duration").Value);
        }

        public string GenerateToken(string id, string firstName, string lastName, string email, string mobile, string gender)
        {
            // Convert the SecretKey to a byte array
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));

            // Create signing credentials using the key
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create claims
            var claims = new[]
            {
                new Claim("UserID", id),
                new Claim("FirstName", firstName),
                new Claim("LastName", lastName),
                new Claim("Email", email),
                new Claim("Mobile", mobile),
                new Claim("Gender", gender)
            };

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(TokenDuration),
                signingCredentials: credentials
            );

            // Return the serialized token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
