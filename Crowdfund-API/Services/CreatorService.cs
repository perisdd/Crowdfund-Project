using Crowdfund.DB;
using Crowdfund.Models;
using Crowdfund_API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund_API.Services
{
	public class CreatorService : ICreatorService
	{
		private readonly FundDbContext _context;

		public CreatorService(FundDbContext context)
		{
			_context = context;
		}

		public async Task<CreatorDTO> GetCreator(int id)
		{
			var creator = await _context.Creators.SingleOrDefaultAsync(c => c.Id == id);

			if (creator == null)
				throw new Exception("Invalid ID.");

			return creator.Convert();
		}

		public async Task<List<CreatorDTO>> GetAllCreators()
		{
			return await _context.Creators.Select(c => c.Convert()).ToListAsync();
		}

		public async Task<CreatorDTO> AddCreator(CreatorDTO creatorDTO)
		{
			if (creatorDTO.FirstName == null || creatorDTO.LastName == null || creatorDTO.Email == null)
				throw new Exception("Required Attributes not Provided.");

			var creator = new Creator()
			{
				Id = creatorDTO.Id,
				FirstName = creatorDTO.FirstName,
				LastName = creatorDTO.LastName,
				Email = creatorDTO.Email,

				ProjectsCreated = new List<Project>()
			};

			_context.Creators.Add(creator);
			await _context.SaveChangesAsync();

			return creator.Convert();
		}

		public async Task<List<CreatorDTO>> Search(string? search)
		{
			IQueryable<Creator> results = _context.Creators;

			if (search == null)
				throw new Exception("Search Term not Provided.");

			results = results.Where(c =>
				c.FirstName.ToLower().Contains(search.ToLower()) ||
				c.LastName.ToLower().Contains(search.ToLower()) ||
				c.Email.ToLower().Contains(search.ToLower())
			);

			if (!results.Any())
				throw new Exception("No Matches Found.");

			return await results.Select(c => c.Convert()).ToListAsync();
		}

		public async Task<CreatorDTO> Update(int id, CreatorDTO creatorDTO)
		{
			var creator = await _context.Creators.SingleOrDefaultAsync(c => c.Id == id);

			if (creator == null)
				throw new Exception("Invalid ID.");

			if (creatorDTO.FirstName != null)
				creator.FirstName = creatorDTO.FirstName;
			if (creatorDTO.LastName != null)
				creator.LastName = creatorDTO.LastName;
			if (creatorDTO.Email != null)
				creator.Email = creatorDTO.Email;

			await _context.SaveChangesAsync();

			return creator.Convert();
		}

		public async Task<string> Delete(int id)
		{
			var creator = await _context.Creators.SingleOrDefaultAsync(c => c.Id == id);

			if (creator == null || creator.ProjectsCreated.Any())
				throw new Exception("Invalid ID or Active Projects.");

			_context.Remove(creator);
			await _context.SaveChangesAsync();

			return "Successful Deletion.";
		}
	}
}
