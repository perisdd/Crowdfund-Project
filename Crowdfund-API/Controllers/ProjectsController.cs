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
        public async Task<ActionResult<ProjectDTO?>> Post(ProjectDTO? projectDTO)
        {
            if (projectDTO is null) return BadRequest("Please provide a valid project");
            
            ProjectDTO result = await _service.AddProject(projectDTO);
  
            if (result is null) return NotFound("Invalid Creator ID.");

            return Ok(result);
        }


        [HttpGet, Route("search")]
        public async Task<ActionResult<List<ProjectDTO?>>> Search(string? title, string? creatorFirst, string? creatorLast)
        {
            var result = await _service.SearchByName(title, creatorFirst, creatorLast);
            if (result is null || !result.Any()) return BadRequest("No projects that match the specified criteria were found.");

            return Ok(result);
        }


        [HttpPatch, Route("update")]
        public async Task<ActionResult<ProjectDTO?>> Update(int id, ProjectDTO projectDTO)
        {

            var result = await _service.UpdateProject(id,projectDTO);

            if (result is null) return BadRequest("No previous project was found to update.");

            return Ok("Project updated successfully!");
        }


        [HttpDelete, Route("delete")]
        public async Task<ActionResult<bool>> Delete(int id)
        {

            var result = await _service.DeleteProject(id);

            if (result) return Ok("Project deleted successfully!");

            return BadRequest("No project was found to delete.");
         
        }

    }
}
