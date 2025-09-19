using FreeSql;
using LS.ERP.Domain;
using LS.ERP.Domain.Shared;
using System.Linq.Expressions;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 泛型领域服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DomainService<TEntity> : DomainService<TEntity, long>, IDomainService<TEntity>
        where TEntity : class, IEntity<long>, new()
    {
        public DomainService(IGenericRepository<TEntity, long> repository)
            : base(repository)
        {

        }
    }

    /// <summary>
    /// 泛型领域服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class DomainService<TEntity, TKey> : IDomainService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TKey : struct, IEquatable<TKey>
    {
        /// <summary>
        /// 仓储
        /// </summary>
        protected readonly IGenericRepository<TEntity, TKey> repository;

        /// <summary>
        /// FreeSql
        /// </summary>
        public IFreeSql FreeSql => repository.Client;

        /// <summary>
        /// Select
        /// </summary>
        public ISelect<TEntity> Select => FreeSql.Select<TEntity>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        public DomainService(IGenericRepository<TEntity, TKey> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            var result = await repository.InsertAsync(entity);
            return entity;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(params TEntity[] entities)
        {
            var result = await repository.InsertAsync(entities);
            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> ModifyAsync(TEntity entity)
        {
            return await repository.ModifyAsync(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = await repository.ModifyAsync(entity);
            return entity;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(TKey id)
        {
            return await repository.DeleteAsync(id);
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<TEntity> GetByKeyAsync(TKey id)
        {
            return repository.GetByKeyAsync(id);
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="id"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<TReturn> FindAsync<TReturn>(TKey id, Expression<Func<TEntity, TReturn>> expression) where TReturn : class
        {
            return repository.FindAsync(id, expression);
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return repository.GetListAsync(predicate);
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<List<TReturn>> GetListAsync<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> expression) where TReturn : class
        {
            return repository.GetListAsync(predicate, expression);
        }

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderExpressions"></param>
        /// <returns></returns>
        public virtual Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20, params Expression<Func<TEntity, object>>[] orderExpressions)
        {
            return repository.GetPagedListAsync(predicate, pageIndex, pageSize, orderExpressions);
        }

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
        public virtual Task<IPagedList<TReturn>> GetPagedListAsync<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> returnExpression, int pageIndex = 1, int pageSize = 20, params Expression<Func<TEntity, object>>[] orderExpressions) where TReturn : class
        {
            return repository.GetPagedListAsync(predicate, returnExpression, pageIndex, pageSize, orderExpressions);
        }
    }
}
