using PrettyLove.Domain.Shared;
using System.Linq.Expressions;

namespace PrettyLove.Application
{
    /// <summary>
    /// 泛型领域服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IAppService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TKey : struct, IEquatable<TKey>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> InsertAsync(params TEntity[] entities);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> ModifyAsync(TEntity entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(TKey id);

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByKeyAsync(TKey id);

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="id"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TReturn> FindAsync<TReturn>(TKey id, Expression<Func<TEntity, TReturn>> expression) where TReturn : class;

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<List<TReturn>> GetListAsync<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> expression) where TReturn : class;

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderExpressions"></param>
        /// <returns></returns>
        Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20, params Expression<Func<TEntity, object>>[] orderExpressions);

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="returnExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderExpressions"></param>
        /// <returns></returns>
        Task<IPagedList<TReturn>> GetPagedListAsync<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> returnExpression, int pageIndex = 1, int pageSize = 20, params Expression<Func<TEntity, object>>[] orderExpressions) where TReturn : class;
    }

    /// <summary>
    /// 泛型领域服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IAppService<TEntity> : IAppService<TEntity, long>
       where TEntity : class, IEntity<long>, new()
    {

    }
}
