using Crowdfund.Models;
using Crowdfund_API.DTOs;
using Crowdfund_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfund_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        public IProjectService _service;

        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<ProjectDTO>> Get()
        {
            return await _service.GetAllProjects();
        }

        [HttpGet, Route("{id}")]
        public async Task<ActionResult<ProjectDTO>> Get(int id)
        {
            var projectDTO = await _service.GetProject(id);
            if (projectDTO == null) { return NotFound("Invalid ID."); }
            
            return Ok(projectDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> Post(ProjectDTO projectDTO)
        {
            ProjectDTO result = await _service.AddProject(projectDTO);
            if (result == null) { return NotFound("Invalid Creator ID."); }

            return Ok(result);
        }
    }
}
