using BlogPost.Server.Database;
using BlogPost.Server.Interface.Interface;
using BlogPost.Server.Model.Domain;
using System.Threading.Tasks;

namespace BlogPost.Server.Interface.Implimentaion
{
    public class BlogpostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;
        public BlogpostRepository(ApplicationDbContext dbContext) { 
            this.dbContext = dbContext;
        
        }
        public async Task<Blogposts> CreateAsync
            (Blogposts blogposts)
        {
            await dbContext.Blogpost.AddAsync(blogposts);
             await dbContext.SaveChangesAsync();
            return blogposts;
           
        }
    }
}
