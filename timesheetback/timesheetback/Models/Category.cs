using System;
namespace timesheetback.Models
{
	public class Category
	{

		public long Id { get; set; }
        public string? Name { get; set; }

        public Category(long id, string? name)
        {
            Id = id;
            Name = name;
        }

        public Category()
		{
		}
	}
}

