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
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(ProductDbContext db) : base (db) { }

        public async Task<Address> GetAddressBySupplier(Guid supplierId)
        {
            return await DbSet.AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(x => x.SupplierId.Equals(supplierId));
        }
    }
}
