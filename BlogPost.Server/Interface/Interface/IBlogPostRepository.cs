using BlogPost.Server.Model.Domain;
using System.Threading.Tasks;

namespace BlogPost.Server.Interface.Interface
{
    public interface IBlogPostRepository
    {

        Task<Blogposts> CreateAsync(Blogposts blogposts);
    }
}
