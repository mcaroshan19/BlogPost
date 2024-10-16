using BlogPost.Server.Database;
using BlogPost.Server.Model.Domain;


using Microsoft.EntityFrameworkCore; // Required for EF Core async extensions


using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Tokens;

using System;


using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Options;


namespace BlogPost.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SingUpController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly string _connectionString;
        private readonly JwtOption _options;


        public SingUpController(ApplicationDbContext context, IConfiguration configuration, IOptions<JwtOption> options)
        {
            _context = context;
            _connectionString= configuration.GetConnectionString("DbContextConnection");
            _options = options.Value;

        }


        //private string GetJWTToken(string email)
        //{
        //    var jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        //    var crendential = new SigningCredentials(jwtKey, SecurityAlgorithms.HmacSha256);
        //    List<Claim> claims = new List<Claim>()
        //    {
        //        new Claim("Email",email),

        //    };
        //    var sToken = new JwtSecurityToken(_options.Key, _options.Issuer, claims, expires: DateTime.Now.AddHours(5), signingCredentials: crendential);
        //    var token = new JwtSecurityTokenHandler().WriteToken(sToken);
        //    return token;
        //}



        private string GetJWTToken(string email)
        {
            var jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var credential = new SigningCredentials(jwtKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim> { new Claim(ClaimTypes.Email, email) };

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: credential
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpGet]
        public IActionResult Get()
        {
            List<Singup> employees = new List<Singup>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetAllSignups", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Singup employee = new Singup
                                {
                                    Username = reader["Username"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Password= reader["Password"].ToString()
                                };
                                employees.Add(employee);
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok(employees); // Return the list of employees
        }



        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Singup model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest(new { error = "Invalid request." });

            //var user = await _context.SingupApp.FirstOrDefaultAsync(u => u.Email == model.Email);

            var user = _context.SingupApp.Where(u => u.Email==model.Email &u.Password==model.Password).FirstOrDefault();
            if (user == null)
                return BadRequest(new { error = "Email does not exist." });

            if (!PasswordHasher.VerifyPassword(model.Password, user.Password))
                return BadRequest(new { error = "Email/password is incorrect." });

            var token = GetJWTToken(user.Email);
            return Ok(new { token });
        }








        [HttpPost("register")]
        public async Task<ActionResult<Singup>> PostSignup(Singup signup)
        {
            // Validate the model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the email already exists
            var existingUser = await _context.SingupApp.FirstOrDefaultAsync(u => u.Email == signup.Email);
            if (existingUser != null)
            {
                return BadRequest(new { error = "Email is already in use." });
            }

            // Password hashing
            signup.Password = PasswordHasher.HashPassword(signup.Password);

            // Add the user to the database
            await _context.SingupApp.AddAsync(signup);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a success response
            return CreatedAtAction(nameof(PostSignup), new { id = signup.SingupID }, new
            {
                Message = "User Registered!",
                Email = signup.Email // Optionally return the email or other details
            });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            // Validate the ID
            if (id <= 0)
            {
                return BadRequest(new { error = "Invalid user ID." });
            }

            // Retrieve the user by ID
            var user = await _context.SingupApp.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { error = "User not found." });
            }

            // Remove the user from the database
            _context.SingupApp.Remove(user);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a success response
            return NoContent(); // 204 No Content indicates success without content
        }









    }
}
