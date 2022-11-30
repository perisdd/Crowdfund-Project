namespace Crowdfund.Models
{
    public class Contribution
    {
        public int Id { get; set; }
        public decimal? Amount { get; set; }

        Backer? _backer;
 
        Project? _project;

        //public List<Project> Projects { get; set; }
        //public List<Backer>? Backers { get; set; }
    }
}
