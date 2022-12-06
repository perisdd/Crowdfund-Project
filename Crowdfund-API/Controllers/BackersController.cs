using Crowdfund_API.DTOs;
using Crowdfund_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfund_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BackersController : ControllerBase
	{
		private readonly IBackerService _service;

		public BackersController(IBackerService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult<List<BackerDTO>>> Get()
		{
			return await _service.GetAllBackers();
		}

		[HttpGet, Route("{id}")]
		public async Task<ActionResult<BackerDTO>> Get(int id)
		{
			try
			{
				var response = await _service.GetBacker(id);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet, Route("Search")]
		public async Task<ActionResult<List<BackerDTO>>> Search(string? search)
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
		public async Task<ActionResult<BackerDTO>> Post(BackerDTO backerDTO)
		{
			try
			{
				var result = await _service.AddBacker(backerDTO);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch, Route("{id}")]
		public async Task<ActionResult<BackerDTO>> Patch([FromRoute] int id, [FromBody] BackerDTO backerDTO)
		{
			try
			{
				var response = await _service.Update(id, backerDTO);
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
