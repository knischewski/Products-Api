using Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // if i forget to map some string entity, make them all varchar(100)
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);

            // disabling cascading deletion - Check why did not work
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            //    .SelectMany(e => e.GetForeignKeys())) 
            //    relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
    }
}
