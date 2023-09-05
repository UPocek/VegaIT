using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using timesheetback.DTOs;
using timesheetback.Services;

namespace timesheetback.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
	{

        private readonly IRoleService _roleService;
		public RoleController(IRoleService roleService)
		{
            _roleService = roleService;
		}

        [HttpGet("all")]
        public async Task<List<RoleDTO>> GetAllRoles()
        {
            return await _roleService.GetAllRolesAsync();
        }
    }
}

