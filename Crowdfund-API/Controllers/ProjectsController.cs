using Crowdfund_API.DTOs;
using Crowdfund_API.Exceptions;
using Crowdfund_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfund_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectsController : ControllerBase
	{
		private IProjectService _service;

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
			var projectDTO = await _service.GetProject(id);
			if (projectDTO == null) { return NotFound("Invalid ID."); }
			
			return Ok(projectDTO);
		}

		[HttpGet, Route("Search")]
		public async Task<ActionResult<List<ProjectDTO>>> Search(string search)
		{
			var response = await _service.Search(search);
			if (response == null) return NotFound("No Match Found.");

			return response;
		}

		[HttpPost]
		public async Task<ActionResult<ProjectDTO>> Post(ProjectDTO projectDTO)
		{
			ProjectDTO result = await _service.AddProject(projectDTO);
			// ...
			return Ok(result);
		}

		[HttpPatch, Route("{id}")]
		public async Task<ActionResult<ProjectDTO>> Patch([FromRoute] int id, [FromBody] ProjectDTO projectDTO)
		{
			try
			{
				var response = await _service.Update(id, projectDTO);
				return Ok(response);
			}
			catch (AggregateException e)
			{
				foreach (var exception in e.InnerExceptions)
				{
					if (exception is NotFoundException)
						return BadRequest(e.Message);
					// ...
				}
			}

			return StatusCode(500);
		}

		[HttpPut, Route("{id}")]
		public async Task<ActionResult<ProjectDTO>> Put([FromRoute] int id, [FromBody] ProjectDTO projectDTO)
		{
			try
			{
				var response = await _service.Replace(id, projectDTO);
				return Ok(response);
			}
			catch (AggregateException e)
			{
				foreach (var exception in e.InnerExceptions)
				{
					if (exception is NotFoundException)
						return BadRequest(e.Message);
				}
			}

			return StatusCode(500);
		}

		[HttpDelete, Route("{id}")]
		public async Task<ActionResult<bool>> Delete(int id)
		{
			return await _service.Delete(id);
		}
	}
}
