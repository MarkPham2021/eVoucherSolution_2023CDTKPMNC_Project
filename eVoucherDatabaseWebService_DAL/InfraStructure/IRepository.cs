using System.Linq.Expressions;

namespace eVoucher_DAL.InfraStructure
{
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        Task<T> Add(T entity);

        // Marks an entity as modified
        Task<T> Update(T entity);

        // Marks an entity to be removed
        Task<T> Delete(T entity);

        Task<T> Delete(int id);

        //Delete multi records
        Task<List<T>> DeleteMulti(Expression<Func<T, bool>> where);

        // Get an entity by int id
        Task<T?> GetSingleById(int id);

        Task<T?> GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);
    }
}