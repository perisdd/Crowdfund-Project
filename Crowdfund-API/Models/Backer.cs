namespace Crowdfund_API.Models
{
    public class Backer : User
    {
        public List<Project> ProjectsInvested { get; set; } = new List<Project>();
        public List<Contribution> Contributions { get; set; } = new List<Contribution>();

    }
}
