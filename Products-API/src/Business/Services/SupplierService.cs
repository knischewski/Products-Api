using Business.Interfaces;
using Business.Models;
using Business.Models.Validations;

namespace Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        public async Task Add(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidation(), supplier)
                && !ExecuteValidation(new AddressValidation(), supplier.Address)) return;

        }

        public async Task Update(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidation(), supplier)) return;
        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address)) return;
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }




    }
}
