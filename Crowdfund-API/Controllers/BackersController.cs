using Crowdfund_API.DTOs;
using Crowdfund_API.Exceptions;
using Crowdfund_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfund_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BackersController : ControllerBase
	{
		private IBackerService _service;

		public BackersController(IBackerService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult<List<BackerDTO>>> Get()
		{
			var response = await _service.GetAllBackers();
			return response;
		}

		[HttpGet, Route("{id}")]
		public async Task<ActionResult<BackerDTO>> Get(int id)
		{
			var response = await _service.GetBacker(id);
			return response;
		}

		[HttpGet, Route("Search")]
		public async Task<ActionResult<List<BackerDTO>>> Search(string firstName, string lastName)
		{
			var response = await _service.Search(firstName, lastName);
			if (response == null) return NotFound("No Match Found.");

			return response;
		}

		[HttpPost]
		public async Task<ActionResult<BackerDTO>> Post(BackerDTO backerDTO)
		{
			BackerDTO result = await _service.AddBacker(backerDTO);
			return Ok(result);
		}

		[HttpPatch, Route("{id}")]
		public async Task<ActionResult<BackerDTO>> Patch([FromRoute] int id, [FromBody] BackerDTO backerDTO)
		{
			try
			{
				var response = await _service.Update(id, backerDTO);
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

		[HttpPut, Route("{id}")]
		public async Task<ActionResult<BackerDTO>> Put([FromRoute] int id, [FromBody] BackerDTO backerDTO)
		{
			try
			{
				var response = await _service.Replace(id, backerDTO);
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
