using Crowdfund_API.DTOs;
using Crowdfund_API.Exceptions;
using Crowdfund_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfund_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CreatorsController : ControllerBase
	{
		private ICreatorService _service;

		public CreatorsController(ICreatorService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult<List<CreatorDTO>>> Get()
		{
			return await _service.GetAllCreators();
		}

		[HttpGet, Route("{id}")]
		public async Task<ActionResult<CreatorDTO>> Get(int id)
		{
			var creatorDTO = await _service.GetCreator(id);
			if (creatorDTO == null) { return NotFound("Invalid ID."); }
			
			return Ok(creatorDTO);
		}

		[HttpGet, Route("Search")]
		public async Task<ActionResult<List<CreatorDTO>>> Search(string search)
		{
			var response = await _service.Search(search);
			if (response == null) return NotFound("No Match Found.");

			return response;
		}

		[HttpPost]
		public async Task<ActionResult<CreatorDTO>> Post(CreatorDTO creatorDTO)
		{
			CreatorDTO result = await _service.AddCreator(creatorDTO);
			// ...
			return Ok(result);
		}

		[HttpPatch, Route("{id}")]
		public async Task<ActionResult<CreatorDTO>> Patch([FromRoute] int id, [FromBody] CreatorDTO creatorDTO)
		{
			try
			{
				var response = await _service.Update(id, creatorDTO);
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
		public async Task<ActionResult<CreatorDTO>> Put([FromRoute] int id, [FromBody] CreatorDTO creatorDTO)
		{
			try
			{
				var response = await _service.Replace(id, creatorDTO);
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
