namespace Crowdfund.Models
{
    public class Contribution
    {
        public int Id { get; set; }
        public decimal? Amount { get; set; }

        public Backer? Backer { get; set; }
 
        public Project? Project { get; set; }

    //public List<Project> Projects { get; set; }
    //public List<Backer>? Backers { get; set; }
}
}
