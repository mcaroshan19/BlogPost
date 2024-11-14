using BlogPost.Server.Database;
using BlogPost.Server.Model.Domain;
using BlogPost.Server.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationFormController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NavigationFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/NavigationForm
        [HttpPost("UserReg")]
        public async Task<IActionResult> CreateUser([FromForm] UserDto userDto, IFormFile file)
         {
            if (userDto == null)
            {
                return BadRequest("User data is null.");
            }

            string uploadPath = Path.Combine("uploads"); 
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath); 
            }

            if (file != null)
            {
                var filePath = Path.Combine(uploadPath, file.FileName); // Ensure you're using the correct path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                userDto.FilePath = filePath; // Store the path in your DTO
            }

            var user = new Userr
            {
                FullName = userDto.FullName,
                Dob = userDto.Dob,
                FilePath = userDto.FilePath,
                Employeee = new Employeee
                {
                    EmployeeCode = userDto.EmployeeCode
                },
                JobDetails = new JobDetails
                {
                    JobTitle = userDto.JobTitle
                },
                BankDetails = new BankDetails
                {
                    BankName = userDto.BankName
                }
            };

            _context.Userss.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
        }



        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetUserById(int id)
        //{
        //    var user = await _context.Userss.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(user);
        //}



        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Userss
                .Include(u => u.Employeee)
                .Include(u => u.JobDetails)
                .Include(u => u.BankDetails)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var completeUser = new CompleteUserDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                FilePath = user.FilePath,
                Dob = user.Dob,
                EmployeeCode = user.Employeee?.EmployeeCode,
                JobTitle = user.JobDetails?.JobTitle,
                BankName = user.BankDetails?.BankName
            };

            return Ok(completeUser);
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Userss.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Userss.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content on successful deletion
        }
    }
}
