using BlogPost.Server.Interface.Implimentaion;
using BlogPost.Server.Interface.Interface;
using BlogPost.Server.Model.Domain;
using BlogPost.Server.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BlogPost.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogpostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogpostsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        [HttpPost]
       
        public async  Task<IActionResult> CreateBlogPost([FromBody] CreateBlogpostsRequestDto request)
        {
            //Convert DTO to Domain model

            var blogposts = new Blogposts
            {
                Title               = request.Title,
                ShortDescription    = request.ShortDescription,
                Content             = request.Content,
                UrlHandle           = request.UrlHandle,
                FeaturedImageUrl    = request.FeaturedImageUrl,
                PublishedDate       = request.PublishedDate,
                Author              = request.Author,
                Ticket            =request.Ticket, 
                IsVisible           = request.IsVisible


            };
            blogposts= await blogPostRepository.CreateAsync(blogposts);

            //conver domain model to DTO


            var response = new BlogpostDto
            {
                   Id                    =  blogposts.Id,
                   Title                 =  blogposts.Title,
                   ShortDescription      =  blogposts.ShortDescription,
                   Content               =  blogposts.Content,
                   UrlHandle             =  blogposts.UrlHandle,
                   FeaturedImageUrl      =  blogposts.FeaturedImageUrl,
                   PublishedDate         =  blogposts.PublishedDate,
                   Author                =  blogposts.Author,
                   Ticket                = blogposts.Ticket,   
                   IsVisible             = blogposts.IsVisible


            };
            return Ok(response);





        }
    }
}
