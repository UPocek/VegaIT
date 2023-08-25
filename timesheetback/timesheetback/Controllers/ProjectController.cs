using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using timesheetback.Services;
using timesheetback.DTOs;

namespace timesheetback.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController :ControllerBase
	{

        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
		{
            _projectService = projectService;
		}

        [HttpGet("all")]
        public async Task<List<ProjectDTO>> GetAllProjects()
        {
            return await _projectService.GetAllProjectsAsync();
        }

        [HttpGet("all-minimal")]
        public async Task<List<ProjectMinimalDTO>> GetAllProjectsMinimalInfo()
        {
            return await _projectService.GetAllProjectsMinimalAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> CreateProject(CreateProjectCredentialsDTO projectCredentials)
        {
            try {
                return await _projectService.CreateProjectAsync(projectCredentials);
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectDTO>> UpdateProject(long id, CreateProjectCredentialsDTO projectCredentials)
        {
            try
            {
                return await _projectService.UpdateProjectAsync(id, projectCredentials);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(long id)
        {
            try
            {
                await _projectService.DeleteProjectAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}

