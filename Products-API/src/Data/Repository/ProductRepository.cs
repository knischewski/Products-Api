using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ProductDbContext db) : base(db) { }

        public async Task<Product> GetProductSupplier(Guid id)
        {
            return await DbSet.AsNoTrackingWithIdentityResolution()
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<Product>> GetProductsSuppliers()
        {
            return await DbSet.AsNoTrackingWithIdentityResolution()
                .Include(p => p.Supplier)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsBySupplier(Guid supplierId)
        {
            return await GetEntities(x => x.SupplierId.Equals(supplierId));
        }
    }
}
