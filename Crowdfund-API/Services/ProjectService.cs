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

        public async Task<ProjectDTO> AddProject(ProjectDTO projectDTO)
        {
            Creator? creator = await _context.Creators.SingleOrDefaultAsync(c => c.Id == projectDTO.Creator.Id);

            if (creator == null) return null;

            Project project = new Project()
            {
                Id = projectDTO.Id,
                Title = projectDTO.Title,
                Description = projectDTO.Description,
                Creator = creator,
                ProjectCategory = projectDTO.Category,
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


        public async Task<List<ProjectDTO>> GetAllProjects()
        {
            return await _context.Projects.Include(c => c.Creator).Select(p => p.Convert()).ToListAsync();
        }

        public async Task<ProjectDTO> GetProject(int projectId)
        {
            var project = await _context.Projects.Include(c => c.Creator).SingleOrDefaultAsync(p => p.Id == projectId);

            if (project == null) return null;

            return project.Convert();
        }

        
        public async Task<ProjectDTO?> UpdateProject(int projectId, ProjectDTO projectDTO)
        {
            Project? project = await _context.Projects.Include(creat => creat.Creator).SingleOrDefaultAsync();

            if (project is null)
                throw new NotFoundException("Invalid ID.");

            if (projectDTO.Title != null) 
                project.Title = projectDTO.Title;
            if (projectDTO.Description != null)
                project.Description = projectDTO.Description;
            if (projectDTO.Goal != 0.0m)
                project.Goal = projectDTO.Goal;
            if (projectDTO.Category != null)
                project.ProjectCategory = projectDTO.Category;
            if (projectDTO.Creator.FirstName != null)
                project.Creator.FirstName = projectDTO.Creator.LastName;
            if (projectDTO.Creator.FirstName != null)
                project.Creator.LastName = projectDTO.Creator.FirstName;
            if (projectDTO.Contributions != null)
                project.Contributions = projectDTO.Contributions;

            await _context.SaveChangesAsync();

            return project.Convert();
        }

        // needs thought
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

            };*/
        }
        

        public async Task<List<ProjectDTO?>> SearchByName(string? title, string? creatorFirst, string? creatorLast)
        {
            IQueryable<Project> result = _context.Projects.Include(creat => creat.Creator);
            
            if (title is not null) result = result.Where(proj => proj.Title!.Contains(title.ToLower()));
            if (creatorFirst is not null) result = result.Where(cre => cre.Creator.FirstName!.ToLower() == creatorFirst.ToLower());
            if (creatorLast is not null) result = result.Where(crea => crea.Creator.LastName!.ToLower() == creatorLast.ToLower());

            List<Project> projects = await result.ToListAsync();
            List<ProjectDTO> projectDTOs = new List<ProjectDTO>();

            foreach (var project in projects)
            {
                projectDTOs.Add(new ProjectDTO()
                {
                    Id = project.Id,
                    Title = project.Title,
                    Description = project.Description,
                    Goal = project.Goal,
                    Contributions = project.Contributions
                });                
            }

            
            return projectDTOs;
        }

        
        public async Task<bool> DeleteProject(int projectId)
        {
            var project = _context.Projects.SingleOrDefault(proj => proj.Id == projectId);

            if (project == null) return false;

            _context.Projects.Remove(project);

            await _context.SaveChangesAsync();
            
            return true;
        }


    }
}
