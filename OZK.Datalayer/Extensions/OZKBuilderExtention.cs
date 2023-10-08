using Microsoft.EntityFrameworkCore;
using OZK.Datalayer.Contracts;

namespace OZK.Datalayer.Extensions
{
    public static class OZKBuilderExtention
    {
        public static void SetupColumnId<T>(this ModelBuilder modelBuilder) where T : class, IEntity
        {
            modelBuilder.Entity<T>()
                .Property(o => o.Id).HasDefaultValueSql("NEWID()");
        }
    }

}

