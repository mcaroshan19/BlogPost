using BlogPost.Server.Model.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;  // Use this for SQL operations
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BlogPost.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoredProcController : ControllerBase
    {
        private readonly string _connectionString;

        // Inject the configuration to access the connection string
        public StoredProcController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbContextConnection");
        }

        [HttpPost]
        public IActionResult Post(StoredProc employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee data is null");
            }

            string msg;
            try
            {
                // Use SqlConnection to communicate with the database
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERTPROC", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", employee.Name);
                        cmd.Parameters.AddWithValue("@Age", employee.Age);
                        cmd.Parameters.AddWithValue("@Active", employee.Active);

                        con.Open();
                        int result = cmd.ExecuteNonQuery(); // Execute the stored procedure
                        con.Close();

                        msg = result > 0 ? "Data has been inserted" : "Error inserting data";
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok(msg);  // Return the result message
        }

        // GET api/storedproc
        [HttpGet]
        public IActionResult Get()
        {
            List<StoredProc> employees = new List<StoredProc>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetStoredproc", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StoredProc employee = new StoredProc
                                {
                                    Name = reader["Name"].ToString(),
                                    Age = Convert.ToInt32(reader["Age"]),
                                    Active = Convert.ToInt32(reader["Active"]),
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


    }
}
