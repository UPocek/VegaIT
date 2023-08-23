﻿using System;
namespace timesheetback.Models
{
	public class Category
	{

		public long Id { get; set; }
        public string? Name { get; set; }

        public Category()
		{
		}

        public Category(long id, string? name)
        {
            Id = id;
            Name = name;
        }

    }
}

