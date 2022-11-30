using Crowdfund.Models;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.DB
{
	public class FundDbContext : DbContext
	{
		public DbSet<Backer>? Backers { get; set; }
		
		public DbSet<Contribution>? Contributions { get; set; }

		public DbSet<Creator>? Creators { get; set; }

		public DbSet<Project>? Projects { get; set; }

		public DbSet<Reward>? Rewards { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			string connection = "Data Source = (local); Initial Catalog = FundProject; Integrated Security = true;";
			optionsBuilder.UseSqlServer(connection);
		}

	}
}
