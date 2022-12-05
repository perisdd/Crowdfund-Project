using Crowdfund.Models;

namespace Crowdfund_API.DTOs
{
	public class ProjectDTO
	{
		public int Id { get; set; }

		public string? Title { get; set; }

		public string? Description { get; set; }

		public CreatorDTO? Creator { get; set; } = null!;

		public Category? Category { get; set; }

		public decimal Contributions { get; set; } = 0.00m;

		public decimal Goal { get; set; } = 0.00m;

		public DateTime CreationDate { get; set; }

		public List<RewardDTO>? Rewards { get; set; }

		public List<BackerDTO>? Backers { get; set; }
	}
}