using System;
using timesheetback.DTOs;

namespace timesheetback.Services
{
	public interface ICategoryService
	{

        List<CategoryDTO> GetAllCategories();
        Task<List<CategoryDTO>> GetAllCategoriesAsync();

        CategoryDTO CreateCategory(CreateCategoryCredentialsDTO CategoryCredentials);
        Task<CategoryDTO> CreateCategoryAsync(CreateCategoryCredentialsDTO CategoryCredentials);

        CategoryDTO UpdateCategory(long id, CreateCategoryCredentialsDTO CategoryCredentials);
        Task<CategoryDTO> UpdateCategoryAsync(long id, CreateCategoryCredentialsDTO CategoryCredentials);

        void DeleteCategory(long id);
        Task DeleteCategoryAsync(long id);

    }
}

