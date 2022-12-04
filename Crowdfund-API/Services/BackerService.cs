using Crowdfund.DB;
using Crowdfund.Models;
using Crowdfund_API.DTOs;
using Crowdfund_API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund_API.Services
{
	public class BackerService : IBackerService
	{
		private readonly FundDbContext _context;

		public BackerService(FundDbContext context)
		{
			_context = context;
		}

		public async Task<BackerDTO> GetBacker(int id)
		{
			var backer = await _context.Backers.SingleOrDefaultAsync(b => b.Id == id);
			return backer.Convert();
		}

		public async Task<List<BackerDTO>> GetAllBackers()
		{
			return await _context.Backers.Select(b => b.Convert()).ToListAsync();
		}

		[HttpPost]
		public async Task<BackerDTO> AddBacker(BackerDTO backerDTO)
		{
			Backer backer = new Backer()
			{
				Id = backerDTO.Id, // ?
				FirstName = backerDTO.FirstName,
				LastName = backerDTO.LastName,
				Email = backerDTO.Email
				// ...
			};

			_context.Backers.Add(backer);
			await _context.SaveChangesAsync();

			return backer.Convert();
		}

		public async Task<List<BackerDTO>> Search(string search)
		{
			IQueryable<Backer> results = _context.Backers;

			if (search != null)
			{
				results = results.Where(b =>
					b.FirstName.ToLower().Contains(search.ToLower()) ||
					b.LastName.ToLower().Contains(search.ToLower()) ||
					b.Email.ToLower().Contains(search.ToLower())
					// ...
				);
			}

			return await results.Select(b => b.Convert()).ToListAsync();
		}

		public async Task<BackerDTO> Update(int id, BackerDTO backerDTO)
		{
			Backer backer = await _context.Backers.SingleOrDefaultAsync(b => b.Id == id);

			if (backer == null)
				throw new NotFoundException("Invalid ID.");

			if (backerDTO.FirstName != null)
				backer.FirstName = backerDTO.FirstName;
			if (backerDTO.LastName != null)
				backer.LastName = backerDTO.LastName;
			if (backerDTO.Email != null)
				backer.Email = backerDTO.Email;
			// ...

			await _context.SaveChangesAsync();

			return backer.Convert();
		}

		public async Task<BackerDTO> Replace(int id, BackerDTO backerDTO)
		{
			Backer backer = await _context.Backers.SingleOrDefaultAsync(b => b.Id == id);

			if (backer == null)
				throw new NotFoundException("Invalid ID.");

			backer.FirstName = backerDTO.FirstName;
			backer.LastName = backerDTO.LastName;
			backer.Email = backerDTO.Email;
			// ...

			await _context.SaveChangesAsync();

			return backer.Convert();
		}

		public async Task<bool> Delete(int id)
		{
			Backer backer = await _context.Backers.SingleOrDefaultAsync(b => b.Id == id);

			if (backer == null || backer.ProjectsInvested != null) // ?
			{
				// Add Message
				return false;
			}

			_context.Remove(backer);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
