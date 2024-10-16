using BlogPost.Server.Database;
using BlogPost.Server.Interface.Interface;
using BlogPost.Server.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BlogPost.Server.Interface.Implimentaion
{
    public class CategoryRepository : IcategoryReposetory

    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext DbContext)
        {
            this.dbContext = DbContext;
        }

        internal static Task GetById(int v, object id)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category> DeleteAsync(int id)
        {

            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCategory is null)
            {
                //dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                //await dbContext.SaveChangesAsync();
                return null;
            }
            dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
            return existingCategory;

        }

        public async Task<IEnumerable<Category>> Getallasync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsyncc(int Id)
        {
           return await dbContext.Categories.FirstOrDefaultAsync(x =>x.Id == Id);
        }

        //public async Task<Category> UpdateAsync(Category category)
        //{
        //    var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
        //    if (existingCategory != null)
        //    {
        //        dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
        //        await dbContext.SaveChangesAsync();
        //        return category;
        //    }

        //}

        public async Task<Category> UpdateAsync(Category category)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (existingCategory != null)
            {
                dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;
            }

            // Throw an exception if the category was not found
            throw new KeyNotFoundException($"Category with ID {category.Id} was not found.");
        }

    }
}
