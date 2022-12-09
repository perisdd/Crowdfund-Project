using Crowdfund_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund_API.DB
{
    public class FundDbContext : DbContext
    {
        public DbSet<Backer> Backers { get; set; }

        public DbSet<Contribution> Contributions { get; set; }

        public DbSet<Creator> Creators { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Reward> Rewards { get; set; }

        public FundDbContext(DbContextOptions<FundDbContext> options) : base(options) { }

    }
}
