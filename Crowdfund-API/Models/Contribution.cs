using System.ComponentModel.DataAnnotations;

namespace Crowdfund_API.Models
{
    public class Contribution
    {
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; } 

        public Backer? Backer { get; set; }
 
        public Project? Project { get; set; }

        [Required]
        public int BackerId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        //public List<Project> Projects { get; set; }
        //public List<Backer>? Backers { get; set; }
    }
}
