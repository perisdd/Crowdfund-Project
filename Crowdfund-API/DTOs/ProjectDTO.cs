using Crowdfund_API.Models;

namespace Crowdfund_API.DTOs
{
	public class ProjectDTO
	{
		public int Id { get; set; }

		public string? Title { get; set; }

		public string? Description { get; set; }

		public CreatorDTO? Creator { get; set; }

		// public int CreatorId { get; set; }

		public Category Category { get; set; }

		public decimal Contributions { get; set; }

		public decimal Goal { get; set; }

		public DateTime CreationDate { get; set; }

		public List<Reward>? Rewards { get; set; }
	}
}