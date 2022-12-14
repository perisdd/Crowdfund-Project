using Crowdfund_API.DTOs;

namespace Crowdfund_API.Services
{
	public interface ICreatorService
	{
		public Task<CreatorDTO> GetCreator(int id);
		public Task<List<CreatorDTO>> GetAllCreators();
		public Task<CreatorDTO> AddCreator(CreatorDTO creatorDTO);
		public Task<List<CreatorDTO>> Search(string? search);
		public Task<CreatorDTO> Update(int id, CreatorDTO creatorDTO);
		public Task<string> Delete(int id);
	}
}
