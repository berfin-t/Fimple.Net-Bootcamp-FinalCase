using BankSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Persistence.Context
{
    public class BankingDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<LoginModel> LoginModels { get; set; }
        public DbSet<UserRoleModel> Roles { get; set; }
        public DbSet<AccountModel> Accounts { get; set; }

        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.LoginModels)
                .WithOne(a => a.Users)
                .HasForeignKey(a => a.UserId);

        }
        }

}
