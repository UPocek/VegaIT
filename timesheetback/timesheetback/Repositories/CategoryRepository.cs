using System;

using timesheetback.Models;
using timesheetback.DTOs;
using Microsoft.EntityFrameworkCore;

namespace timesheetback.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{

        private readonly TimeSheetContext _context;

		public CategoryRepository(TimeSheetContext context)
		{
            _context = context;
		}

        public void DeleteCategory(long id)
        {
            var categoryToDelete = _context.Categories.Find(id) ?? throw new Exception("Category with that id does not exist");
            _context.Categories.Remove(categoryToDelete);
            _context.SaveChanges();
        }

        public async Task DeleteCategoryAsync(long id)
        {
            var categoryToDelete = await _context.Categories.FirstOrDefaultAsync(category => category.Id == id) ?? throw new Exception("Category with that id does not exist");
            _context.Categories.Remove(categoryToDelete);
            _context.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Task<List<Category>> GetAllCategoriesAsync()
        {
            return _context.Categories.ToListAsync();
        }

        public Category? GetCategoryById(long id)
        {
            return _context.Categories.Find(id);
        }

        public Task<Category?> GetCategoryByIdAsync(long id)
        {
            return _context.Categories.FirstOrDefaultAsync(category => category.Id == id);
        }

        public Category? GetCategoryByName(string name)
        {
            return _context.Categories.FirstOrDefault(category => category.Name == name);
        }

        public Task<Category?> GetCategoryByNameAsync(string name)
        {
            return _context.Categories.FirstOrDefaultAsync(category => category.Name == name);
        }

        public Category SaveCategory(Category newCategory)
        {
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return newCategory;
        }

        public Category UpdateCategory(Category categoryToUpdate, CreateCategoryCredentialsDTO categoryCredentials)
        {
            categoryToUpdate.Name = categoryCredentials.Name;
            _context.SaveChanges();
            return categoryToUpdate;
        }
    }
}

