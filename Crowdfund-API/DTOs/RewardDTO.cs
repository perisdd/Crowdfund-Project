namespace Crowdfund_API.DTOs
{
    public class RewardDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public int ProjectId { get; set; }
    }
}
