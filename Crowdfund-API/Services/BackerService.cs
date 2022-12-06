using Crowdfund.DB;
using Crowdfund.Models;
using Crowdfund_API.DTOs;
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

			if (backer == null)
				throw new Exception("Invalid ID.");
			
			return backer.Convert();
		}

		public async Task<List<BackerDTO>> GetAllBackers()
		{
			return await _context.Backers.Select(b => b.Convert()).ToListAsync();
		}

		public async Task<BackerDTO> AddBacker(BackerDTO backerDTO)
		{
			if (backerDTO.FirstName == null || backerDTO.LastName == null || backerDTO.Email == null)
				throw new Exception("Required Attributes not Provided.");

			var backer = new Backer()
			{
				Id = backerDTO.Id,
				FirstName = backerDTO.FirstName,
				LastName = backerDTO.LastName,
				Email = backerDTO.Email,

				ProjectsInvested = new List<Project>(),

				// remove contributions from model and from here
				// because these are stored separately in DB context
				Contributions = new List<Contribution>()
			};

			_context.Backers.Add(backer);
			await _context.SaveChangesAsync();

			return backer.Convert();
		}

		public async Task<List<BackerDTO>> Search(string? search)
		{
			IQueryable<Backer> results = _context.Backers;

			if (search == null)
				throw new Exception("Search Term not Provided.");
			
			results = results.Where(b =>
				b.FirstName.ToLower().Contains(search.ToLower()) ||
				b.LastName.ToLower().Contains(search.ToLower()) ||
				b.Email.ToLower().Contains(search.ToLower())
			);

			if (!results.Any())
				throw new Exception("No Matches Found.");

			return await results.Select(b => b.Convert()).ToListAsync();
		}

		public async Task<BackerDTO> Update(int id, BackerDTO backerDTO)
		{
			var backer = await _context.Backers.SingleOrDefaultAsync(b => b.Id == id);

			if (backer == null)
				throw new Exception("Invalid ID.");

			if (backerDTO.FirstName != null)
				backer.FirstName = backerDTO.FirstName;
			if (backerDTO.LastName != null)
				backer.LastName = backerDTO.LastName;
			if (backerDTO.Email != null)
				backer.Email = backerDTO.Email;
			
			await _context.SaveChangesAsync();

			return backer.Convert();
		}

		public async Task<string> Delete(int id)
		{
			var backer = await _context.Backers.SingleOrDefaultAsync(b => b.Id == id);

			if (backer == null || backer.ProjectsInvested.Any())
				throw new Exception("Invalid ID or Active Contributions.");

			_context.Backers.Remove(backer);
			await _context.SaveChangesAsync();

			return "Successful Deletion.";
		}
	}
}
