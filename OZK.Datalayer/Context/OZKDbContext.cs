using Microsoft.EntityFrameworkCore;
using OZK.Datalayer.Domain;
using OZK.Datalayer.Extensions;

namespace OZK.Datalayer.Context
{
    public class OZKDbContext : ConfigConnectionContextBase
    {
        private const string _CONNECTION_KEY = "OZKDbConnection";
        public OZKDbContext() : base(_CONNECTION_KEY)
        {
        }

        public OZKDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        //Organization
        public DbSet<BankUser> OZKBankUser { get; set; }
        public DbSet<BankAccount> OZKBankAccount { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.SetupColumnId<BankUser>();
            modelBuilder.SetupColumnId<BankAccount>();


            //modelBuilder.Entity<BankAccount>()
            //    .HasOne(e => e.)
            //    .WithMany(u => u.)
            //    .HasForeignKey("BankUserID")
            //    .OnDelete(DeleteBehavior.NoAction
            //    
            modelBuilder.Entity<BankUser>()
            .HasMany(o => o.BankAccounts);
          



            //.WithOne(o => o.BankUser)

        }
    }

}

