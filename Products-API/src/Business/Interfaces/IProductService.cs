using Business.Models;

namespace Business.Interfaces
{
    public interface IProductService
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(Guid id);
    }
}
