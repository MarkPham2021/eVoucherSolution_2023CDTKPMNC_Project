using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_DAL.InfraStructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private eVoucherDbContext _context;
        private DbSet<T> _dbSet;
        public RepositoryBase(eVoucherDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public virtual async Task<T> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<T> Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        public virtual bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Count(predicate) > 0;
        }

        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return _dbSet.Count(where);
        }

        public virtual async Task<T> Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) { return null; }
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<List<T>> DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where(where).AsEnumerable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
            await _context.SaveChangesAsync();
            var ListOfEntities = _dbSet.ToList();    //list of remain entities
            return ListOfEntities;
        }

        public virtual IEnumerable<T> GetAll()
        {
            var objects = _dbSet.AsQueryable();
            return objects;
        }

        public IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _context.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where(predicate).AsQueryable();
            }

            return _context.Set<T>().Where(predicate).AsQueryable();
        }

        public IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _context.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = filter != null ? query.Where(filter).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = filter != null ? _context.Set<T>().Where(filter).AsQueryable() : _context.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public async Task<T?> GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbSet.Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return await query.FirstOrDefaultAsync(expression);
            }
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<T?> GetSingleById(int id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
