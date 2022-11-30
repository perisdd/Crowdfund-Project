namespace Crowdfund.Models
{
    public class Reward : Entity
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        Project? _project;
        //public List<Backer>? Backers { get; set; }
        //public List<Project> Projects { get; set; }
    }
}
