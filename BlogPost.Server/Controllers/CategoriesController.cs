
using BlogPost.Server.Interface.Implimentaion;
using BlogPost.Server.Interface.Interface;
using BlogPost.Server.Model.Domain;
using BlogPost.Server.Model.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;




namespace BlogPost.Server.Controllers
{

    
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IcategoryReposetory _categoryRepository;

        public CategoriesController(IcategoryReposetory CategoryRepository)
        {
            _categoryRepository = CategoryRepository;
        }

        public object Id { get; private set; }

        //https://localhost:7169/api/Categories

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            try
            {

                var category = new Category
                {

                    Name = request.Name,
                    UrlHandle = request.UrlHandle
                };

                await _categoryRepository.CreateAsync(category);



                var response = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                };

                return Ok(response);
            }

            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Error creating category: {ex.Message}");
                Console.WriteLine(ex.StackTrace);

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating the category." });
            }
        }

        //get category api
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var Categories = await _categoryRepository.Getallasync();

            // map domain model to dto
            var response = new List<CategoryDto>();
            foreach (var category in Categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }
            return Ok(response);
        }
        // GET: api/Categories/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ExistingCAtegoires = await _categoryRepository.GetByIdAsyncc(id);

            if (ExistingCAtegoires == null)
            {
                return NotFound();
            }
            var Response = new CategoryDto
            {
                Id = ExistingCAtegoires.Id,
                Name = ExistingCAtegoires.Name,
                UrlHandle = ExistingCAtegoires.UrlHandle

            };
            return Ok(Response);
        }

        // PUT: api/Categories/{id}
        [HttpPut("{id:int}")]
        public  async Task<IActionResult> Edditcategory([FromRoute] int id , UpdateCategoryRequestDto request)
        {
            // Conver DTO domain model 

            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            category = await _categoryRepository.UpdateAsync(category);

            if(category == null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);

        }

        // Delete: api/Categories/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id )
        {
            var category = await _categoryRepository.DeleteAsync(id);
            if (category is null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                Id = category.Id,
               
            };
            return Ok(response);

        }

    }
}

