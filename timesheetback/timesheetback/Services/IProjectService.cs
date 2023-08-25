using System;
using timesheetback.DTOs;

namespace timesheetback.Services
{
	public interface IProjectService
	{

		List<ProjectDTO> GetAllProjects();
        Task<List<ProjectDTO>> GetAllProjectsAsync();

		List<ProjectMinimalDTO> GetAllProjectsMinimal();
		Task<List<ProjectMinimalDTO>> GetAllProjectsMinimalAsync();

		ProjectDTO CreateProject(CreateProjectCredentialsDTO projectCredentials);
		Task<ProjectDTO> CreateProjectAsync(CreateProjectCredentialsDTO projectCredentials);

		ProjectDTO UpdateProject(long id, CreateProjectCredentialsDTO projectCredentials);
		Task<ProjectDTO> UpdateProjectAsync(long id, CreateProjectCredentialsDTO projectCredentials);

		void DeleteProject(long id);
		Task DeleteProjectAsync(long id);

    }
}

