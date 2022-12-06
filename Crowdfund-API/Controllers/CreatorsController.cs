using Crowdfund_API.DTOs;
using Crowdfund_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfund_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CreatorsController : ControllerBase
	{
		private readonly ICreatorService _service;

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
			try
			{
				var response = await _service.GetCreator(id);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet, Route("Search")]
		public async Task<ActionResult<List<CreatorDTO>>> Search(string? search)
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
		public async Task<ActionResult<CreatorDTO>> Post(CreatorDTO creatorDTO)
		{
			try
			{
				var result = await _service.AddCreator(creatorDTO);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch, Route("{id}")]
		public async Task<ActionResult<CreatorDTO>> Patch([FromRoute] int id, [FromBody] CreatorDTO creatorDTO)
		{
			try
			{
				var response = await _service.Update(id, creatorDTO);
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
