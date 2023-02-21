using InsuranceClaimApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsuranceClaimApi.DbContexts
{
    public class InsuranceContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "name=ConnectionStrings:InsuranceClaimApiDBConnectionString");
        }

        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Claim> Claims { get; set; } = null!;
    }
}
