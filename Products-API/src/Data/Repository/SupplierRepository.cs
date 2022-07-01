using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ProductDbContext db) : base(db)
        {
        }

        public async Task<Supplier> GetSupplierAddress(Guid id)
        {
            return await DbSet.AsNoTrackingWithIdentityResolution()
                .Include(x => x.Address)
                .FirstOrDefaultAsync(f => f.Id.Equals(id));
        }

        public async Task<Supplier> GetSupplierProductsAddress(Guid id)
        {
            return await DbSet.AsNoTrackingWithIdentityResolution()
                .Include(s => s.Address)
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id.Equals(id));
        }
    }
}
