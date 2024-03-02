namespace Carlton.Core.Foundation.API.Data.Repository;

public interface IRepository<T, TId> : IReadOnlyRepository<T, TId>
{
    Task Update(T entity);
    Task Insert(T entity);
    Task Delete(T entity);
}
