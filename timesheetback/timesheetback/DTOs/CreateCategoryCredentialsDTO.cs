using System;
using timesheetback.Models;

namespace timesheetback.DTOs
{
	public class CreateCategoryCredentialsDTO
	{
        public string Name { get; set; }

        public CreateCategoryCredentialsDTO()
		{
		}

        public CreateCategoryCredentialsDTO(string name)
        {
            Name = name;
        }

        public CreateCategoryCredentialsDTO(Category category) {
            Name = category.Name;
        }
    }
}

