using Business.Models;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(Guid id);
        Task<IEnumerable<TEntity>> GetEntities(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
