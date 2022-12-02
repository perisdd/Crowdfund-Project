﻿namespace Crowdfund.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public string UserName { get; set; } = "";

        public string Password { get; set; } = "";

        public UserType Role { get; set; } 

    }

    public enum UserType 
    {
        BACKER,
        CREATOR
    } 
}
