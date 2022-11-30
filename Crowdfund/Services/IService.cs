using Crowdfund.Models;

namespace Crowdfund.Services
{
	public interface IService
	{
		public void AddCreator(Creator creator);

		public void RemoveCreator(Creator creator);

		public void AddBacker(Backer backer);

		public void RemoveBacker(Backer backer);

		public void AddProject(Project project);

		public void RemoveProject(Project project);

		public void AddReward(Reward reward);

		public void RemoveReward(Reward reward);

		public void AddContribution(Contribution contribution);
	}
}
