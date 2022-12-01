namespace Crowdfund.Models
{
    public class Backer : User
    {
        public List<Project> projectsInvested { get; set; } = new List<Project>();
        public List<Contribution> contributions { get; set; }
    }
}
