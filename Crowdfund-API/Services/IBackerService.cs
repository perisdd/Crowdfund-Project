using Crowdfund_API.DTOs;

namespace Crowdfund_API.Services
{
	public interface IBackerService
	{
		public Task<BackerDTO> GetBacker(int id);
		public Task<List<BackerDTO>> GetAllBackers();
		public Task<BackerDTO> AddBacker(BackerDTO backerDTO);
		public Task<List<BackerDTO>> Search(string search);
		public Task<BackerDTO> Update(int id, BackerDTO backerDTO);
		public Task<BackerDTO> Replace(int id, BackerDTO backerDTO);
		public Task<bool> Delete(int id);

		// public Task<bool> AddContribution(int id, ...);
	}
}
