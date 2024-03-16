using BankSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Persistence.Context
{
    public class BankingDbContext : DbContext
    {
        public DbSet<UserModel> User { get; set; }
        public DbSet<LoginModel> Login { get; set; }
        public DbSet<AccountModel> Account { get; set; }
        public DbSet<TransactionModel> Transaction { get; set; }
        public DbSet<LoanApplicationModel> LoanApplication { get; set; }
        public DbSet<LoanModel> Loan { get; set; }

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

            modelBuilder.Entity<LoanApplicationModel>()
                .HasOne(l => l.User)
                .WithMany(user => user.LoanApplications)
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<LoanModel>()
                .HasOne(l => l.User)
                .WithMany(user => user.Loans)
                .HasForeignKey(l => l.UserId);

            base.OnModelCreating(modelBuilder);

        }
        }

}
