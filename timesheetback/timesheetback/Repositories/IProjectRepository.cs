using System;
using timesheetback.DTOs;
using timesheetback.Models;

namespace timesheetback.Repositories
{
	public interface IProjectRepository
	{

		List<Project> GetAllProjects();
		Task<List<Project>> GetAllProjectsAsync();

		Project? GetProjectByName(string name);
		Task<Project?> GetProjectByNameAsync(string name);

		Project? GetProjectById(long id);
		Task<Project?> GetProjectByIdAsync(long id);

		void DeleteProject(long id);
		Task DeleteProjectAsync(long id);

        Project SaveProject(Project newProject);
		Project UpdateProject(Project project, CreateProjectCredentialsDTO projectCredentials);

    }
}

