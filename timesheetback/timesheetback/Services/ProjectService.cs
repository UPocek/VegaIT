using System;
using System.Linq;
using timesheetback.DTOs;
using timesheetback.Models;
using timesheetback.Repositories;

namespace timesheetback.Services
{
	public class ProjectService : IProjectService
	{
        private readonly IProjectRepository _projectRepository;
        private readonly List<string> validProjectStatuses = new() { "active", "inactive", "archive" };

        public ProjectService(IProjectRepository projectRepository)
		{
            _projectRepository = projectRepository;
		}

        public ProjectDTO CreateProject(CreateProjectCredentialsDTO projectCredentials)
        {
            if (_projectRepository.GetProjectByName(projectCredentials.Name) != null || !validProjectStatuses.Contains(projectCredentials.Status))
            {
                throw new Exception("Project with that name already exists");
            }

            var newProject = new Project(projectCredentials);

            return new ProjectDTO(_projectRepository.SaveProject(newProject));
        }

        public async Task<ProjectDTO> CreateProjectAsync(CreateProjectCredentialsDTO projectCredentials)
        {
            if (await _projectRepository.GetProjectByNameAsync(projectCredentials.Name) != null || !validProjectStatuses.Contains(projectCredentials.Status))
            {
                throw new Exception("Project with that name already exists");
            }

            var newProject = new Project(projectCredentials);

            return new ProjectDTO(_projectRepository.SaveProject(newProject));
        }

        public void DeleteProject(long id)
        {
            _projectRepository.DeleteProject(id);
        }

        public async Task DeleteProjectAsync(long id)
        {
            await _projectRepository.DeleteProjectAsync(id);
        }

        public List<ProjectDTO> GetAllProjects()
        {
            List<Project> allEProjects = _projectRepository.GetAllProjects();
            return allEProjects.Select(project => new ProjectDTO(project)).ToList();
        }

        public async Task<List<ProjectDTO>> GetAllProjectsAsync()
        {
            List<Project> allProjects = await _projectRepository.GetAllProjectsAsync();
            return allProjects.Select(project => new ProjectDTO(project)).ToList();
        }

        public List<ProjectMinimalDTO> GetAllProjectsMinimal()
        {
            List<Project> allProjects = _projectRepository.GetAllProjects();
            return allProjects.Select(project => new ProjectMinimalDTO(project)).ToList();
        }

        public async Task<List<ProjectMinimalDTO>> GetAllProjectsMinimalAsync()
        {
            List<Project> allProjects = await _projectRepository.GetAllProjectsAsync();
            return allProjects.Select(project => new ProjectMinimalDTO(project)).ToList();
        }

        public ProjectDTO UpdateProject(long id, CreateProjectCredentialsDTO projectCredentials)
        {
            Project projectToUpdate = _projectRepository.GetProjectById(id) ?? throw new Exception("Project with that id does not exist");
            return new ProjectDTO(_projectRepository.UpdateProject(projectToUpdate, projectCredentials));
        }

        public async Task<ProjectDTO> UpdateProjectAsync(long id, CreateProjectCredentialsDTO projectCredentials)
        {
            Project projectToUpdate = await _projectRepository.GetProjectByIdAsync(id) ?? throw new Exception("Project with that id does not exist");
            return new ProjectDTO(_projectRepository.UpdateProject(projectToUpdate, projectCredentials));
        }
    }
}

