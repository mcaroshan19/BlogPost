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
        public DbSet<User> Users
        { get; set; }





        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .Property(u => u.Membersince)
        //        .HasDefaultValueSql("GETDATE()");
        //}



    }
}
