using Crowdfund_API.DTOs;

namespace Crowdfund_API.Services
{
    public interface IProjectService
    {
        public Task<ProjectDTO?> AddProject(ProjectDTO projectDTO);

        public Task<List<ProjectDTO?>> GetAllProjects();

        public Task<ProjectDTO?> GetProject(int projectId);

        public Task<ProjectDTO?> UpdateProject(int projectId, ProjectDTO projectDTO);

        public Task<ProjectDTO?> AddRewards(int projectId, RewardDTO projectDTO);

        public Task<List<ProjectDTO?>> SearchByName(string? title, string? creatorFirst, string? creatorLast);

        public Task<bool> DeleteProject(int projectId);

    }
}
