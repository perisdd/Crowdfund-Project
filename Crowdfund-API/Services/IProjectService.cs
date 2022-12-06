﻿using Crowdfund_API.DTOs;

namespace Crowdfund_API.Services
{
    public interface IProjectService
    {
		public Task<ProjectDTO> GetProject(int id);
		public Task<List<ProjectDTO>> GetAllProjects();
		public Task<ProjectDTO> AddProject(ProjectDTO projectDTO);
		public Task<List<ProjectDTO>> Search(string? search);
		public Task<ProjectDTO> Update(int id, ProjectDTO projectDTO);
		public Task<string> Delete(int id);
	}
}
