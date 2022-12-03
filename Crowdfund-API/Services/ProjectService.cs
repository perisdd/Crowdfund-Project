using Crowdfund.Models;
using Crowdfund_API.DB;
using Crowdfund_API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund_API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly FundDbContextAPI _context;

        public ProjectService(FundDbContextAPI context)
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
    }
}
