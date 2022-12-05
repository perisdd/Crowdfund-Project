using Crowdfund.DB;
using Crowdfund.Models;
using Crowdfund_API.DTOs;
using Crowdfund_API.Exceptions;
using Microsoft.AspNetCore.Mvc;
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

			return creator.Convert();
		}

		public async Task<List<CreatorDTO>> GetAllCreators()
		{
			return await _context.Creators.Select(c => c.Convert()).ToListAsync();
		}

		public async Task<CreatorDTO> AddCreator(CreatorDTO creatorDTO)
		{
			Creator creator = new Creator()
			{
				Id = creatorDTO.Id, // ?
				FirstName = creatorDTO.FirstName,
				LastName = creatorDTO.LastName,
				Email = creatorDTO.Email
				// ...
			};

			_context.Creators.Add(creator);
			await _context.SaveChangesAsync();

			return creator.Convert();
		}

		public async Task<List<CreatorDTO>> Search(string search)
		{
			IQueryable<Creator> results = _context.Creators;

			if (search != null)
			{
				results = results.Where(c =>
					c.FirstName.ToLower().Contains(search.ToLower()) ||
					c.LastName.ToLower().Contains(search.ToLower()) ||
					c.Email.ToLower().Contains(search.ToLower())
					// ...
				);
			}

			return await results.Select(c => c.Convert()).ToListAsync();
		}

		public async Task<CreatorDTO> Update(int id, CreatorDTO creatorDTO)
		{
			Creator creator = await _context.Creators.SingleOrDefaultAsync(c => c.Id == id);

			if (creator == null)
				throw new NotFoundException("Invalid ID.");

			if (creatorDTO.FirstName != null)
				creator.FirstName = creatorDTO.FirstName;
			if (creatorDTO.LastName != null)
				creator.LastName = creatorDTO.LastName;
			if (creatorDTO.Email != null)
				creator.Email = creatorDTO.Email;
			// ...

			await _context.SaveChangesAsync();

			return creator.Convert();
		}

		public async Task<CreatorDTO> Replace(int id, CreatorDTO creatorDTO)
		{
			Creator creator = await _context.Creators.SingleOrDefaultAsync(c => c.Id == id);

			if (creator == null)
				throw new NotFoundException("Invalid ID.");

			creator.FirstName = creatorDTO.FirstName;
			creator.LastName = creatorDTO.LastName;
			creator.Email = creatorDTO.Email;
			// ...

			await _context.SaveChangesAsync();
			return creator.Convert();
		}

		public async Task<bool> Delete(int id)
		{
			Creator creator = await _context.Creators.SingleOrDefaultAsync(c => c.Id == id);

			if (creator == null || creator.projectsCreated != null)
			{
				// Add Message
				return false;
			}

			_context.Remove(creator);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
