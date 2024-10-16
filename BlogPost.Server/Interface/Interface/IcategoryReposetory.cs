using BlogPost.Server.Model.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPost.Server.Interface.Interface
{
    public interface IcategoryReposetory
    {

        Task<Category> CreateAsync(Category category);

        Task<IEnumerable<Category>> Getallasync();
        Task<Category?> GetByIdAsyncc(int Id);
        Task<Category?> UpdateAsync(Category category);
        Task<Category?> DeleteAsync(int id);

    }
}
