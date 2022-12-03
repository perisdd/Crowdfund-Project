using Crowdfund.Models;

namespace Crowdfund_API.DTOs
{
    public static class DTOConverters
    {
        public static ProjectDTO Convert(this Project project)
        {
            return new ProjectDTO()
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Creator = new CreatorDTO()
                {
                    Id = project.Creator.Id,
                    FirstName = project.Creator.FirstName,
                    LastName = project.Creator.LastName,
                    Email = project.Creator.Email,
                },
                Category = project.ProjectCategory,
                Contributions = project.Contributions,
                Goal = project.Goal,
                CreationDate = project.CreationDate
            };
        }
    }
}
