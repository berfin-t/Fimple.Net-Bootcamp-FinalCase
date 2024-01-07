using BankSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Persistence.Context
{
    public class BankingDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<LoginModel> Login { get; set; }
        public DbSet<AccountModel> Account { get; set; }

        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserModel>()
                .HasMany(a => a.LoginModels)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<AccountModel>()
                .HasOne(account => account.User)
                .WithMany(user => user.Accounts)
                .HasForeignKey(account => account.UserId);

            base.OnModelCreating(modelBuilder);

        }
        }

}
