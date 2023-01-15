using HappyCompany.Infra.Constants;
using System.Linq.Expressions;


namespace HappyCompany.Domain.Interface.Repos
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> criteria, int? skip, int? take, Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);

        Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task Add(TEntity entity);
        Task Update(TEntity entity);

        Task AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
       
    }
}
