using System;
using timesheetback.Models;


namespace timesheetback.DTOs
{
	public class ProjectMinimalDTO
	{
        public long Id { get; set; }
        public string Name { get; set; }


        public ProjectMinimalDTO()
		{
		}

        public ProjectMinimalDTO(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public ProjectMinimalDTO(Project project) {
            Id = project.Id;
            Name = project.Name;
        }
    }
}

