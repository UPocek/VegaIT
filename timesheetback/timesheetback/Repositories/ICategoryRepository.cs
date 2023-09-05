using System;
using timesheetback.DTOs;
using timesheetback.Models;

namespace timesheetback.Repositories
{
	public interface ICategoryRepository
	{

        List<Category> GetAllCategories();
        Task<List<Category>> GetAllCategoriesAsync();

        Category? GetCategoryByName(string name);
        Task<Category?> GetCategoryByNameAsync(string name);

        Category? GetCategoryById(long id);
        Task<Category?> GetCategoryByIdAsync(long id);

        void DeleteCategory(long id);
        Task DeleteCategoryAsync(long id);

        Category SaveCategory(Category newCategory);
        Category UpdateCategory(Category category, CreateCategoryCredentialsDTO categoryCredentials);

    }
}

