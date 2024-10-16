namespace BlogPost.Server.Model.Domain
{
    public class Student
    {

        public int Id { get; set; }            // Primary key
        public string Name { get; set; }       // Student's name
        public string Major { get; set; }      // Major subject
        public decimal GPA { get; set; }
    }
}
