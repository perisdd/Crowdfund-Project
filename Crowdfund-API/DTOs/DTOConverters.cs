using Crowdfund_API.Models;

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
                    Id = project.Creator!.Id,
                    FirstName = project.Creator.FirstName,
                    LastName = project.Creator.LastName,
                    Email = project.Creator.Email,
                },
                Category = project.ProjectCategory,
                Contributions = project.Contributions,
                Goal = project.Goal,
                CreationDate = project.CreationDate,

				Rewards = project.Rewards,
            };
        }

		public static CreatorDTO Convert(this Creator creator)
		{
			return new CreatorDTO()
			{
				Id = creator.Id,
				FirstName = creator.FirstName,
				LastName = creator.LastName,
				Email = creator.Email,
			};
		}

		public static BackerDTO Convert(this Backer backer)
		{
			return new BackerDTO()
			{
				Id = backer.Id,
				FirstName = backer.FirstName,
				LastName = backer.LastName,
				Email = backer.Email,
			};
		}
	}
}
