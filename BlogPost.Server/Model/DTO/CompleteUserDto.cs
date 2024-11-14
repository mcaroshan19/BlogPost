using System;

namespace BlogPost.Server.Model.DTO
{
    public class CompleteUserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string FilePath { get; set; }
        public DateTime Dob { get; set; }
        public string EmployeeCode { get; set; }
        public string JobTitle { get; set; }
        public string BankName { get; set; }


    }
}
