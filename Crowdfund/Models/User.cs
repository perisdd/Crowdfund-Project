namespace Crowdfund.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public string userName { get; set; } = "";

        public string password { get; set; } = "";
    }
}
