﻿using Business.Models;

namespace Business.Interfaces
{
    public interface ISupplierService
    {
        Task Add(Supplier supplier);
        Task Update(Supplier supplier);
        Task Delete(Guid id);
        Task UpdateAddress(Address address);
    }
}
