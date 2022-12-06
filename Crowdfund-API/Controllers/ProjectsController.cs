using Crowdfund_API.DTOs;
using Crowdfund_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfund_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectsController : ControllerBase
	{
		private readonly IProjectService _service;

		public ProjectsController(IProjectService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult<List<ProjectDTO>>> Get()
		{
			return await _service.GetAllProjects();
		}

		[HttpGet, Route("{id}")]
		public async Task<ActionResult<ProjectDTO>> Get(int id)
		{
			try
			{
				var response = await _service.GetProject(id);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet, Route("Search")]
		public async Task<ActionResult<List<ProjectDTO>>> Search(string? search)
		{
			try
			{
				var response = await _service.Search(search);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult<ProjectDTO>> Post(ProjectDTO projectDTO)
		{
			try
			{
				var result = await _service.AddProject(projectDTO);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch, Route("{id}")]
		public async Task<ActionResult<ProjectDTO>> Patch([FromRoute] int id, [FromBody] ProjectDTO projectDTO)
		{
			try
			{
				var response = await _service.Update(id, projectDTO);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete, Route("{id}")]
		public async Task<ActionResult<string>> Delete(int id)
		{
			try
			{
				var response = await _service.Delete(id);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
