using System;
using timesheetback.DTOs;
using timesheetback.Models;
using timesheetback.Repositories;
namespace timesheetback.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

        public CategoryDTO CreateCategory(CreateCategoryCredentialsDTO categoryCredentials)
        {
            if (_categoryRepository.GetCategoryByName(categoryCredentials.Name) != null)
            {
                throw new Exception("Category with that name already exists");
            }

            var newCategory = new Category(categoryCredentials);

            return new CategoryDTO(_categoryRepository.SaveCategory(newCategory));
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CreateCategoryCredentialsDTO categoryCredentials)
        {
            if (await _categoryRepository.GetCategoryByNameAsync(categoryCredentials.Name) != null)
            {
                throw new Exception("Category with that name already exists");
            }

            var newCategory = new Category(categoryCredentials);

            return new CategoryDTO(_categoryRepository.SaveCategory(newCategory));
        }

        public void DeleteCategory(long id)
        {
            _categoryRepository.DeleteCategory(id);
        }

        public async Task DeleteCategoryAsync(long id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
        }

        public List<CategoryDTO> GetAllCategories()
        {
            List<Category> allECategories = _categoryRepository.GetAllCategories();
            return allECategories.Select(category => new CategoryDTO(category)).ToList();
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            List<Category> allCategories = await _categoryRepository.GetAllCategoriesAsync();
            return allCategories.Select(category => new CategoryDTO(category)).ToList();
        }

        public CategoryDTO UpdateCategory(long id, CreateCategoryCredentialsDTO categoryCredentials)
        {
            Category categoryToUpdate = _categoryRepository.GetCategoryById(id) ?? throw new Exception("Category with that id does not exist");
            return new CategoryDTO(_categoryRepository.UpdateCategory(categoryToUpdate, categoryCredentials));
        }

        public async Task<CategoryDTO> UpdateCategoryAsync(long id, CreateCategoryCredentialsDTO categoryCredentials)
        {
            Category categoryToUpdate = await _categoryRepository.GetCategoryByIdAsync(id) ?? throw new Exception("Category with that id does not exist");
            return new CategoryDTO(_categoryRepository.UpdateCategory(categoryToUpdate, categoryCredentials));
        }
    }
}

