namespace Crowdfund.Models
{
    public class Contribution : Entity
    {
        public decimal? Amount { get; set; }

        //public List<Backer>? Backers { get; set; }
        Backer? _backer;
        //public List<Project> Projects { get; set; }
        Project? _project;
    }
}
