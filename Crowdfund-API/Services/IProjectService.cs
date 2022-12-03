using Crowdfund_API.DTOs;

namespace Crowdfund_API.Services
{
    public interface IProjectService
    {
        Task<ProjectDTO> AddProject(ProjectDTO projectDTO);

        Task<List<ProjectDTO>> GetAllProjects();

        Task<ProjectDTO> GetProject(int projectId);

    }
}
