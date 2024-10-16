using BlogPost.Server.Database;
using BlogPost.Server.Model.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var cacheKey = "employeeList";
            string serializedEmployeeList;
            var employeeList = new List<Employee>();

            // Check if data is available in Redis cache
            var redisEmployeeList = await _cache.GetAsync(cacheKey);

            if (redisEmployeeList != null)
            {
                // Deserialize data from Redis cache
                serializedEmployeeList = Encoding.UTF8.GetString(redisEmployeeList);
                employeeList = JsonConvert.DeserializeObject<List<Employee>>(serializedEmployeeList);
            }
            else
            {
                // Fetch data from the database
                employeeList = await _context.Employees.ToListAsync();

                // Serialize and store data in Redis cache
                serializedEmployeeList = JsonConvert.SerializeObject(employeeList);
                redisEmployeeList = Encoding.UTF8.GetBytes(serializedEmployeeList);

                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))   // Expire if inactive for 5 minutes
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)); // Absolute expiration after 10 minutes

                await _cache.SetAsync(cacheKey, redisEmployeeList, options);
            }

            return Ok(employeeList);
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var cacheKey = $"employee_{id}";
            string serializedEmployee;
            Employee employee;

            // Check if data is available in Redis cache
            var redisEmployee = await _cache.GetAsync(cacheKey);

            if (redisEmployee != null)
            {
                // Deserialize data from Redis cache
                serializedEmployee = Encoding.UTF8.GetString(redisEmployee);
                employee = JsonConvert.DeserializeObject<Employee>(serializedEmployee);
            }
            else
            {
                // Fetch data from the database
                employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }

                // Serialize and store data in Redis cache
                serializedEmployee = JsonConvert.SerializeObject(employee);
                redisEmployee = Encoding.UTF8.GetBytes(serializedEmployee);

                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))   // Expire if inactive for 5 minutes
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)); // Absolute expiration after 10 minutes

                await _cache.SetAsync(cacheKey, redisEmployee, options);
            }

            return Ok(employee);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<IActionResult> PostEmployee(Employee employee)
        {
            // Add new employee to the database
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Invalidate cache for employee list to ensure fresh data is fetched
            await _cache.RemoveAsync("employeeList");

            // Return newly created employee with its generated Id
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }
    }
}
