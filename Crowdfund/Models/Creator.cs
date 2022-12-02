namespace Crowdfund.Models
{
    public class Creator: User
    {
        public User? _creator { get; set; }
        List<Project>? projectsCreated { get; set; }

        public void CreateProject() { }
        public void AddProjectTitle() { }
        public void AddDescription() { }
        public void AddPhotos() { }
        public void AddVideos() { }
        public void PostStatusUpdates() { }
        public void TrackFinancialProgress() { }
        public void OfferFundingPackages() { }
    }
}
