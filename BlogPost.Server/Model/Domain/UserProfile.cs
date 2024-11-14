//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text.Json.Serialization;

//namespace BlogPost.Server.Model.Domain
//{
//    public class UserProfile
//    {



//        [ForeignKey("UserID")]
//        public int UserID { get; set; }

//        public string FirstName { get; set; }
//        public string LastName { get; set; }
//        public string Email { get; set; }
//        public string Mobile { get; set; }
//        public string Gender { get; set; }
//        public string Pwd { get; set; }




//        public Userr Userr { get; set; }
//    }
//}







using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BlogPost.Server.Model.Domain
{
    public class UserProfile
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string Pwd { get; set; }
    }
}
    