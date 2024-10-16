
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;

//namespace BlogPost.Server.Database
//{
//    public class AuthDbContext : IdentityDbContext
//    {
//        public AuthDbContext(DbContextOptions options) : base(options)
//        {
//        }
//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            base.OnModelCreating(builder);
//            var readerRolId = "544132107";
//            var writerRolId = "449268759";


//            var roles = new list<IdentityRole>
//            {
//                new IdentityRole {
//                    Id = readerRolId,
//                    Name ="Reader",
//                    NormilizedName="Reader".ToUpper(),
//                    Currencystamp=readerRolId
//                },
//                new IdentityRole
//                {
//                     Id = readerRolId,
//                    Name ="Writer",
//                    NormilizedName="Writer".ToUpper(),
//                    Currencystamp=writerRolId
//                }
//            }
//              builder.Entity<IdentityRole>().HasData(roles);

//            //create admin user
//            var adminUserID = "288801812";
//            var admin = new IdentityUser
//            {
//                Id = adminUserID
//                 UserName="mcaoshan2@gmail.com",
//                Email="mcaroshan2@gmail.com"
//                 NormlizedEmail= "mcaoshan2@gmail.com".ToUpper(),
//                NormilizedUseName= "mcaoshan2@gmail.com".ToLower()
//            };
//        }
//    }
//}

