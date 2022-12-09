using System.ComponentModel.DataAnnotations;

namespace Crowdfund_API.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; } 

        [Required]
        public string? Description { get; set; }

        public string? ImgUrl { get; set; }

		public string? VideoUrl { get; set; }

		public Creator? Creator { get; set; }

        public int CreatorId { get; set; }

        [Required]
        public Category ProjectCategory { get; set; }

		public decimal Contributions { get; set; } = 0.00m;

        [Required]
		public decimal Goal { get; set; } = 0.00m;

        public bool Active { get; set; }

        public DateTime CreationDate { get; set; }

        public List<Reward> Rewards { get; set; } = new List<Reward>();

        public List<Backer> Backers { get; set; } = new List<Backer>();

        // public List<string> StatusUpdates { get; set; } = new List<string>();
        }


}

