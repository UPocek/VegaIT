using System;
using timesheetback.Models;

namespace timesheetback.DTOs
{
	public class CategoryDTO
	{
        public long Id { get; set; }
        public string Name { get; set; }

        public CategoryDTO()
		{
		}

        public CategoryDTO(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public CategoryDTO(Category category) {
            Id = category.Id;
            Name = category.Name;
        }
    }
}

