using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyStore.Core.Domain;
using MyStore.Infrastructure.EF.Configurations;

namespace MyStore.Infrastructure.EF
{
    public class MyStoreContext : DbContext
    {
        private readonly IOptions<SqlOptions> _sqlOptions;

        public DbSet<Product> Products { get; set; }

        public MyStoreContext(IOptions<SqlOptions> sqlOptions)
        {
            _sqlOptions = sqlOptions;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            if (_sqlOptions.Value.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase(_sqlOptions.Value.Database);

                return;
            }

            optionsBuilder.UseSqlServer(_sqlOptions.Value.ConnectionString,
                o => o.MigrationsAssembly("MyStore.Web"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}