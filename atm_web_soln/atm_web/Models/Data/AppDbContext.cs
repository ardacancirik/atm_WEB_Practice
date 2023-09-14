using Microsoft.EntityFrameworkCore;

namespace atm_web.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Account>()
                .HasKey(a => a.AccountNumber);

            

            base.OnModelCreating(modelBuilder);
        }


    }
}
