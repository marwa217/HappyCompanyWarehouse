using HappyCompany.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HappyCompany.Application.Repos
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public ApplicationDbContext context_;
        private DbSet<TEntity> _entities;

        public Repository()
        {
        }

        public Repository(ApplicationDbContext context)
        {
            context_ = context as ApplicationDbContext;
            context_.Database.EnsureCreated();
            _entities = context_.Set<TEntity>();
        }

        public async Task<TEntity> Get(int id)
        {
            try
            {
                return await _entities.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.SingleOrDefaultAsync(predicate);
        }

        public async Task Add(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task Update(TEntity entity)
        {
             _entities.Update(entity);
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _entities.AddRangeAsync(entities);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> criteria, int? skip, int? take,
            Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            var query = _entities.Where(criteria);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return query.ToList();
        }
    }
}


