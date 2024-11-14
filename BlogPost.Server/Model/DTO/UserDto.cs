using Microsoft.AspNetCore.Http;
using System;

namespace BlogPost.Server.Model.DTO
{
    public class UserDto
    {
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public string EmployeeCode { get; set; }
        public string JobTitle { get; set; }
        public string BankName { get; set; }
        public string FilePath { get; set; }
    }
}
