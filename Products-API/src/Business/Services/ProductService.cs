using Business.Interfaces;
using Business.Models;
using Business.Models.Validations;

namespace Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        public async Task Add(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;
        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }


    }
}
