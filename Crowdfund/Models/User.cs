using System.ComponentModel.DataAnnotations;

namespace Crowdfund.Models
{
    public class User : Entity
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Email { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public UserType Role { get; set; }

    }
}
    public enum UserType 
    {
        BACKER,
        CREATOR
    } 

