using BlogPost.Server.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Server.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Blogposts> Blogpost { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Singup> SingupApp {  get; set; }
        //public DbSet<User> Users
        //{ get; set; }

        public DbSet<Userr> Userss { get; set; }
        public DbSet<Employeee> EmployeesExtended { get; set; }
        public DbSet<JobDetails> JobDetailsExtended { get; set; }
        public DbSet<BankDetails> BankDetailsExtended { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Disable Cascade Delete for UserProfile - Userr relationship
        //    modelBuilder.Entity<UserProfile>()
        //        .HasOne(u => u.Userr)
        //        .WithOne(p => p.UserProfile)
        //        .HasForeignKey<UserProfile>(p => p.UserId)
        //        .OnDelete(DeleteBehavior.Restrict); 

        
        //    modelBuilder.Entity<Employeee>()
        //        .HasOne(e => e.Userr)
        //        .WithMany(u => u.Employeee)
        //        .HasForeignKey(e => e.Id)
        //        .OnDelete(DeleteBehavior.Restrict); 

         
        //    modelBuilder.Entity<JobDetails>()
        //        .HasOne(j => j.Userr)
        //        .WithMany(u => u.JobDetails)
        //        .HasForeignKey(j => j.Id)
        //        .OnDelete(DeleteBehavior.Restrict); 


        //    modelBuilder.Entity<BankDetails>()
        //        .HasOne(b => b.Userr)
        //        .WithMany(u => u.BankDetails)
        //        .HasForeignKey(b => b.Id)
        //        .OnDelete(DeleteBehavior.Restrict); 
        //}


    }
}
