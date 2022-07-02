using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class SupplierMapping : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(x => x.Document)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.HasOne(f => f.Address)
                .WithOne(e => e.Supplier);

            builder.HasMany(f => f.Products)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId);

            builder.ToTable("Suppliers");
        }
    }
}
