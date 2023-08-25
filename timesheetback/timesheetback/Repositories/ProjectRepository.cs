using System;
using System.Net;
using Microsoft.EntityFrameworkCore;
using timesheetback.DTOs;
using timesheetback.Models;

namespace timesheetback.Repositories
{
	public class ProjectRepository : IProjectRepository
	{
        private readonly TimeSheetContext _context;

        public ProjectRepository(TimeSheetContext context)
		{
            _context = context;
		}

        public void DeleteProject(long id)
        {
            var projectToDelete = _context.Projects.Find(id) ?? throw new Exception("Project with that id does not exist");
            _context.Projects.Remove(projectToDelete);
            _context.SaveChanges();
        }

        public async Task DeleteProjectAsync(long id)
        {
            var projectToDelete = await _context.Projects.FirstOrDefaultAsync(project => project.Id == id) ?? throw new Exception("Project with that id does not exist");
            _context.Projects.Remove(projectToDelete);
            _context.SaveChanges();
        }

        public List<Project> GetAllProjects()
        {
            return _context.Projects.ToList();
        }

        public Task<List<Project>> GetAllProjectsAsync()
        {
            return _context.Projects.ToListAsync();
        }

        public Project? GetProjectById(long id)
        {
            return _context.Projects.Find(id);
        }

        public Task<Project?> GetProjectByIdAsync(long id)
        {
            return _context.Projects.FirstOrDefaultAsync(project => project.Id == id);
        }

        public Project? GetProjectByName(string name)
        {
            return _context.Projects.FirstOrDefault(project => project.Name == name);
        }

        public Task<Project?> GetProjectByNameAsync(string name)
        {
            return _context.Projects.FirstOrDefaultAsync(project => project.Name == name);
        }

        public Project SaveProject(Project newProject)
        {
            _context.Projects.Add(newProject);
            _context.SaveChanges();
            return newProject;
        }

        public Project UpdateProject(Project projectToUpdate, CreateProjectCredentialsDTO projectCredentials)
        {
            projectToUpdate.Name = projectCredentials.Name;
            projectToUpdate.Description = projectCredentials.Description;
            projectToUpdate.Status = projectCredentials.Status;
            projectToUpdate.EmployeeId = projectCredentials.EmployeeId;
            projectToUpdate.ClientId = projectCredentials.ClientId;

            _context.SaveChanges();

            return projectToUpdate;
        }
    }
}

