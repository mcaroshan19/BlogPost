using BlogPost.Server.Model.Domain;
using System.Threading.Tasks;

namespace BlogPost.Server.Interface.Interface
{
    public interface IUserService
    {

        Task<string> RegisterAsync(RegisterModel model);
        Task<string> LoginAsync(UserLogin model);
    }
}
