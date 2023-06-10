using Microsoft.EntityFrameworkCore;
using webapi.Extensions;

namespace webapi.Entities.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity, new()
    {
        Task<IEnumerable<T>> GetsAsync(string search, int pageSize = 0, int pageNumber = 0);
        Task<T> GetAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteRangeAsync(List<int> ids);
        Task CreateRangeAsync(List<T> entities);
        Task UpdateRangeAsync(List<T> entities);
        Task<T> WriteStatusChangeLog(T entity);
        IQueryable<T> GetAll();
    }

    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        private ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task<T> CreateAsync(T entity)
        {
            entity = await WriteStatusChangeLog(entity);

            await _dbContext.DbSet<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return entity;
        }

        public virtual async Task CreateRangeAsync(List<T> entities)
        {
            await _dbContext.DbSet<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var T = _dbContext.DbSet<T>().SingleOrDefault(t => t.Id == id);

            if (T == null)
                throw new InvalidDataException($"Unable to find T with ID: {id}");

            _dbContext.DbSet<T>().Remove(T);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public virtual async Task DeleteRangeAsync(List<int> ids)
        {
            var T = _dbContext.DbSet<T>().Where(t => ids.Contains(t.Id)).ToList();

            if (T == null)
                throw new InvalidDataException($"Unable to find T with ID: {string.Join(",", ids)}");

            _dbContext.DbSet<T>().RemoveRange(T);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            var query = _dbContext.DbSet<T>().AsQueryable();
            query = Include(query);
            var T = await query.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

            if (T == null)
                throw new InvalidDataException($"Unable to find T with ID: {id}");

            return T;
        }

        public virtual IQueryable<T> Include(IQueryable<T> query)
        {
            return query;
        }

        public virtual async Task<IEnumerable<T>> GetsAsync(string search, int pageSize = 0, int pageNumber = 0)
        {
            var query = _dbContext.DbSet<T>().AsQueryable().AsNoTracking();
            query = this.FilterQuery(query, search);
            query = Include(query);
            var totalCount = await query.CountAsync();
            var entities = await query.PageBy(x => x.Id, pageNumber, pageSize).ToListAsync();

            var pageList = new List<T>();
            //pageList.Data.AddRange(entities);
            //pageList.PaginationDetails.CollectionSize = totalCount;
            //pageList.PaginationDetails.PageIndex = pageNumber;
            //pageList.PaginationDetails.PageSize = pageSize;

            return pageList;
        }

        public virtual IQueryable<T> IncludeUpdate(IQueryable<T> query)
        {
            return query;
        }

        public virtual IQueryable<T> FilterQuery(IQueryable<T> query, string search)
        {
            if (search.IsHasValue())
            {
                query = query.Where(x => x.Id.ToString() == search);
            }
            return query;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            var query = _dbContext.DbSet<T>().AsQueryable();
            query = IncludeUpdate(query);

            if (entity == null)
                throw new InvalidDataException($"Unable to find T with ID: {entity.Id}");

            entity = await WriteStatusChangeLog(entity);

            _dbContext.DbSet<T>().Update(entity);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return entity;
        }

        public virtual async Task UpdateRangeAsync(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> WriteStatusChangeLog(T entity)
        {
            return entity;
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.DbSet<T>().AsQueryable();
        }
    }
}
