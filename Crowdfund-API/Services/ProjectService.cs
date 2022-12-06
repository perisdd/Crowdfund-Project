using Crowdfund.DB;
using Crowdfund.Models;
using Crowdfund_API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund_API.Services
{
	public class ProjectService : IProjectService
	{
		private readonly FundDbContext _context;

		public ProjectService(FundDbContext context)
		{
			_context = context;
		}

		public async Task<ProjectDTO> GetProject(int id)
		{
			var project = await _context.Projects.Include(p => p.Creator).Include(p => p.Rewards).SingleOrDefaultAsync(p => p.Id == id);

			if (project == null)
				throw new Exception("Invalid ID.");

			return project.Convert();
		}

		public async Task<List<ProjectDTO>> GetAllProjects()
		{
			return await _context.Projects.Include(p => p.Creator).Include(p => p.Rewards).Select(p => p.Convert()).ToListAsync();
		}

		public async Task<ProjectDTO> AddProject(ProjectDTO projectDTO)
		{
			if (projectDTO.Title == null || projectDTO.Description == null || projectDTO.Goal == 0 || projectDTO.Creator == null || projectDTO.Creator.Id == 0)
				throw new Exception("Required Attributes not Provided.");

			var creator = await _context.Creators.SingleOrDefaultAsync(c => c.Id == projectDTO.Creator.Id);

			if (creator == null)
				throw new Exception("Invalid Creator ID.");

			var project = new Project()
			{
				Id = projectDTO.Id,
				Title = projectDTO.Title,
				Description = projectDTO.Description,
				ProjectCategory = projectDTO.Category,
				CreationDate = DateTime.Now,

				Creator = creator,

				Contributions = 0.0m,
				Goal = projectDTO.Goal,

				Rewards = new List<Reward>(),
				Backers = new List<Backer>()
			};

			_context.Projects.Add(project);
			await _context.SaveChangesAsync();

			return project.Convert();
		}

		public async Task<List<ProjectDTO>> Search(string? search)
		{
			IQueryable<Project> results = _context.Projects.Include(p => p.Creator);

			if (search == null)
				throw new Exception("Search Term not Provided.");

			results = results.Where(p =>
				p.Title.ToLower().Contains(search.ToLower()) ||
				p.Description.ToLower().Contains(search.ToLower())
			);

			if (!results.Any())
				throw new Exception("No Matches Found.");

			return await results.Select(p => p.Convert()).ToListAsync();
		}

		public async Task<ProjectDTO> Update(int id, ProjectDTO projectDTO)
		{
			var project = await _context.Projects.Include(p => p.Creator).SingleOrDefaultAsync(p => p.Id == id);

			if (project == null)
				throw new Exception("Invalid ID.");

			if (projectDTO.Title != null) 
				project.Title = projectDTO.Title;
			if (projectDTO.Description != null)
				project.Description = projectDTO.Description;
			if (projectDTO.Goal != 0)
				project.Goal = projectDTO.Goal;

			// Must Always Be Set When Making Changes
			project.ProjectCategory = projectDTO.Category;
			
			await _context.SaveChangesAsync();

			return project.Convert();
		}

		public async Task<ProjectDTO> AddReward(int id, RewardDTO rewardDTO)
		{
			if (rewardDTO.Title == null || rewardDTO.Description == null)
				throw new Exception("Required Attributes not Provided.");

			var project = await _context.Projects.Include(p => p.Creator).Include(p => p.Rewards).SingleOrDefaultAsync(p => p.Id == id);

			if (project == null)
				throw new Exception("Invalid ID.");

			var reward = new Reward()
			{
				Id = rewardDTO.Id,
				Title = rewardDTO.Title,
				Description = rewardDTO.Description
			};

			project.Rewards.Add(reward);
			
			_context.Rewards.Add(reward);
			await _context.SaveChangesAsync();

			return project.Convert();
		}

		public async Task<string> RemoveReward(int id, int rewardId)
		{
			var project = _context.Projects.Include(p => p.Rewards).SingleOrDefault(p => p.Id == id);

			if (project == null)
				throw new Exception("Invalid Project ID");

			var reward = project.Rewards.SingleOrDefault(r => r.Id == rewardId);

			if (reward == null)
				throw new Exception("Invalid Reward ID");

			project.Rewards.Remove(reward);
			_context.Rewards.Remove(reward);
			await _context.SaveChangesAsync();

			return "Successful Deletion";
		}

		public async Task<string> Delete(int id)
		{
			var project = _context.Projects.SingleOrDefault(p => p.Id == id);

			if (project == null)
				throw new Exception("Invalid ID");

			// project.Backers.Any() || project.Rewards.Any()

			// What Happens If Project Has Backers or Rewards?

			// For Rewards, Delete With Project
			// --> Set dbo.Rewards.ProjectId ON DELETE CASCADE
			
			// For Backers, Don't Allow Deletion of Project?
			// --> ...

			_context.Projects.Remove(project);
			await _context.SaveChangesAsync();

			return "Successful Deletion.";
		}
	}
}
