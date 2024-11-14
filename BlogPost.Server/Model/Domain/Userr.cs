using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPost.Server.Model.Domain
{
    public class Userr
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public string FilePath { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        [JsonIgnore]
        public Employeee Employeee { get; set; }

        [JsonIgnore]
        public JobDetails JobDetails { get; set; }

        [JsonIgnore]
        public BankDetails BankDetails { get; set; }
    }

    public class Employeee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string EmployeeCode { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public Userr User { get; set; }
    }

    public class JobDetails
    {
        [Key]
        public int JobId { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public Userr User { get; set; }
    }

    public class BankDetails
    {
        [Key]
        public int BankId { get; set; }

        [Required]
        public string BankName { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public Userr User { get; set; }
    }
}
