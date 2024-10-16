
using System;
namespace BlogPost.Server.Model.Domain
{
    public class Blogposts
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string UrlHandle { get; set; }
        public string FeaturedImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public string Ticket { get; set; }
        public bool IsVisible { get; set; }
    }
}
