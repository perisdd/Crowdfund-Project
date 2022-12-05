using Crowdfund.DB;
using Crowdfund.Models;
using Crowdfund_API.DTOs;
using Crowdfund_API.Exceptions;
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
			var project = await _context.Projects.Include(c => c.Creator).SingleOrDefaultAsync(p => p.Id == id);

			if (project == null) return null;

			return project.Convert();
		}

		public async Task<List<ProjectDTO>> GetAllProjects()
		{
			return await _context.Projects.Include(c => c.Creator).Select(p => p.Convert()).ToListAsync();
		}

		public async Task<ProjectDTO> AddProject(ProjectDTO projectDTO)
		{
			Creator creator = await _context.Creators.SingleOrDefaultAsync(c => c.Id == projectDTO.Creator.Id);

			if (creator == null) return null;

			Project project = new Project()
			{
				Id = projectDTO.Id,
				Title = projectDTO.Title,
				Description = projectDTO.Description,
				Creator = creator,
				// ProjectCategory = projectDTO.Category,
				Contributions = projectDTO.Contributions,
				Goal = projectDTO.Goal,
				CreationDate = projectDTO.CreationDate,
				Rewards = new List<Reward>(),
				Backers = new List<Backer>()
			};

			_context.Projects.Add(project);
			await _context.SaveChangesAsync();

			return project.Convert();
		}

		public async Task<List<ProjectDTO>> Search(string search)
		{
			IQueryable<Project> results = _context.Projects.Include(p => p.Creator);

			if (search != null)
			{
				results = results.Where(p =>
					p.Title.ToLower().Contains(search.ToLower()) ||
					p.Description.ToLower().Contains(search.ToLower())
					// ...
				);
			}

			return await results.Select(p => p.Convert()).ToListAsync();
		}

		public async Task<ProjectDTO> Update(int id, ProjectDTO projectDTO)
		{
			Project project = await _context.Projects.Include(p => p.Creator).SingleOrDefaultAsync(p => p.Id == id);

			if (project is null)
				throw new NotFoundException("Invalid ID.");

			if (projectDTO.Title != null) 
				project.Title = projectDTO.Title;
			if (projectDTO.Description != null)
				project.Description = projectDTO.Description;
			if (projectDTO.Goal != 0.0m)
				project.Goal = projectDTO.Goal;
			if (projectDTO.Category != null)
				// project.ProjectCategory = projectDTO.Category;
			if (projectDTO.Creator != null)
			{
				var creator = _context.Creators.SingleOrDefault(c => c.Id == projectDTO.Creator.Id);

				if (creator != null)
					project.Creator = creator;
				else
					throw new NotFoundException("Invalid Creator");
			}
			if (projectDTO.Contributions != 0)
				project.Contributions = projectDTO.Contributions;
			// ...

			await _context.SaveChangesAsync();

			return project.Convert();
		}

		public async Task<ProjectDTO> Replace(int id, ProjectDTO projectDTO)
		{
			Project project = await _context.Projects.Include(p => p.Creator).SingleOrDefaultAsync(p => p.Id == id);

			if (project is null)
				throw new NotFoundException("Invalid ID.");

			project.Title = projectDTO.Title;
			project.Description = projectDTO.Description;
			// ...

			await _context.SaveChangesAsync();
			return project.Convert();
		}

		public async Task<bool> Delete(int id)
		{
			Project project = _context.Projects.SingleOrDefault(p => p.Id == id);

			if (project == null) return false;

			_context.Projects.Remove(project);

			await _context.SaveChangesAsync();

			return true;
		}

		// needs thought
		/*
		public Task<ProjectDTO?> AddRewards(int projectId, RewardDTO rewardDTO)
		{
			throw new NotImplementedException();
			/*
			Project? project = await _context.Projects.Include(creat => creat.Creator).SingleOrDefaultAsync();

			if (project is null) return null;

			ProjectDTO projectDTO. _context.Projects.Where(rewardDTO.ProjectId) == projectId)

			if (rewardDTO.Title != null && rewardDTO.Description != null) 

			return new RewardDTO()
			{
				Id = project.Id,
				Title = project.Title,
				Description = project.Description,
				CreatorId = project.Creator.Id,
				CreatorFirstName = project.Creator.FirstName,
				CreatorLastName = project.Creator.LastName

			};
		}
		*/
	}
}
