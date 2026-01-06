using FreeSql;
using XW.PrettyLove.Domain.Shared;
using System.Linq.Expressions;

namespace XW.PrettyLove.Domain
{
    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TKey : struct, IEquatable<TKey>
    {
        #region 公开属性

        IFreeSql Client { get; }

        #endregion

        #region 数据库与事务

        /// <summary>
        /// 数据库切换
        /// </summary>
        /// <param name="dbEnum"></param>
        /// <returns></returns>
        void SwitchDB(DBEnum dbEnum);

        /// <summary>
        /// 手动提交  作用于DbContext
        /// 同步方法
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// 手机提交  作用于DbContext
        /// 异步方法
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// 开启同线程事务
        /// 不支持异步
        /// </summary>
        /// <param name="action"></param>
        void Transaction(Action action);

        /// <summary>
        /// 工作单元事务提交
        /// 支持异步
        /// </summary>
        /// <returns></returns>
        void Transaction(Action<IFreeSql> action);

        #endregion

        #region 新增数据

        /// <summary>
        /// DbContext 新增数据
        /// 同步方法
        /// </summary>
        /// <param name="data"></param>
        void Add(TEntity data);

        /// <summary>
        /// DbContext 新增数据
        /// 异步方法
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddAsync(TEntity data, CancellationToken cancellationToken = default);

        /// <summary>
        /// DbContext 新增数据
        /// 同步方法
        /// </summary>
        /// <param name="data"></param>
        void AddRange(IEnumerable<TEntity> data);

        /// <summary>
        /// DbContext 新增数据
        /// 异步方法
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddRangeAsync(IEnumerable<TEntity> data, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增一条数据 同步
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ignoreColumns"></param>
        /// <returns></returns>
        int Insert(TEntity entity, params string[] ignoreColumns);

        /// <summary>
        /// 新增一条数据 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="ignoreColumns"></param>
        /// <returns></returns>
        Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default, params string[] ignoreColumns);

        /// <summary>
        /// 新增多条数据 同步
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="ignoreColumns"></param>
        /// <returns></returns>
        int Insert(IEnumerable<TEntity> entities, params string[] ignoreColumns);

        /// <summary>
        /// 新增多条数据 异步
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="ignoreColumns"></param>
        /// <returns></returns>
        Task<int> InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default, params string[] ignoreColumns);

        /// <summary>
        /// 添加或更新数据
        /// </summary>
        /// <param name="entity"></param>
        void AddOrUpdate(TEntity entity);

        #endregion

        #region 修改数据

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <returns></returns>
        IUpdate<TEntity> UpdateEntity();

        /// <summary>
        /// DbContext 修改数据
        /// 同步方法
        /// </summary>
        /// <param name="data"></param>
        void Update(TEntity data);

        /// <summary>
        /// DbContext 修改数据
        /// 异步方法
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity data, CancellationToken cancellationToken = default);

        /// <summary>
        /// DbContext 修改数据
        /// 同步方法
        /// </summary>
        /// <param name="data"></param>
        void UpdateRange(IEnumerable<TEntity> data);

        /// <summary>
        /// DbContext 修改数据
        /// 异步方法
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateRangeAsync(IEnumerable<TEntity> data, CancellationToken cancellationToken = default);

        /// <summary>
        /// 修改数据 同步
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updateColumns"></param>
        /// <returns></returns>
        int Update(TEntity entity, string[] updateColumns);

        /// <summary>
        /// 修改数据 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updateColumns"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity, string[] updateColumns, CancellationToken cancellationToken = default);

        /// <summary>
        /// 修改数据 同步
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="updateColumns"></param>
        /// <returns></returns>
        int Update(IEnumerable<TEntity> entities, string[] updateColumns);

        /// <summary>
        /// 修改数据 异步
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="updateColumns"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(IEnumerable<TEntity> entities, string[] updateColumns, CancellationToken cancellationToken = default);

        /// <summary>
        /// 修改数据 同步
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        int Modify(TEntity entity, Expression<Func<TEntity, object>> expression = null);

        /// <summary>
        /// 修改数据 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> ModifyAsync(TEntity entity, Expression<Func<TEntity, object>> expression = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 修改数据 同步
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        int Modify(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> expression = null);

        /// <summary>
        /// 修改数据 异步
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> ModifyAsync(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> expression = null, CancellationToken cancellationToken = default);

        #endregion

        #region 逻辑删除

        /// <summary>
        /// 逻辑删除 同步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int FakeDelete<Entity>(Entity entity) where Entity : class, IEntity<long>, ISoftDelete;

        /// <summary>
        /// 逻辑删除 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> FakeDeleteAsync<Entity>(Entity entity) where Entity : class, IEntity<long>, ISoftDelete;

        /// <summary>
        /// 逻辑删除 同步
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int FakeDelete<Entity>(IEnumerable<Entity> entities) where Entity : class, IEntity<long>, ISoftDelete;

        /// <summary>
        /// 逻辑删除 异步
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> FakeDeleteAsync<Entity>(IEnumerable<Entity> entities) where Entity : class, IEntity<long>, ISoftDelete;

        #endregion

        #region 物理删除

        /// <summary>
        /// DbContext 删除数据
        /// </summary>
        /// <param name="data"></param>
        void Remove(TEntity data);

        /// <summary>
        /// DbContext 删除数据
        /// </summary>
        /// <param name="data"></param>
        void RemoveRange(IEnumerable<TEntity> data);

        /// <summary>
        /// 删除数据 同步
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// 删除数据 异步
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除数据 同步
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 删除数据 异步
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除数据 同步
        /// </summary>
        /// <param name="dywhere"></param>
        /// <returns></returns>
        int Delete(object dywhere);

        /// <summary>
        /// 删除数据 异步
        /// </summary>
        /// <param name="dywhere"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(object dywhere, CancellationToken cancellationToken = default);

        #endregion

        #region 查询数据

        /// <summary>
        /// ISelect构造
        /// </summary>
        /// <returns></returns>
        ISelect<TEntity> Select();

        /// <summary>
        /// ISelect构造
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISelect<TEntity> Where(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 根据主键查询数据 同步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="id"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        TReturn Find<TReturn>(TKey id, Expression<Func<TEntity, TReturn>> expression) where TReturn : class;

        /// <summary>
        /// 根据主键查询数据 异步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="id"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TReturn> FindAsync<TReturn>(TKey id, Expression<Func<TEntity, TReturn>> expression) where TReturn : class;

        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetByKey(TKey id);

        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByKeyAsync(TKey id);

        /// <summary>
        /// 查询单条数据 同步
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查询单条数据 异步
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查询单条数据 同步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        TReturn Get<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> expression) where TReturn : class;

        /// <summary>
        /// 查询单条数据 异步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TReturn> GetAsync<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> expression) where TReturn : class;

        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Any(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查询列表数据 同步
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查询列表数据 同步
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="maxNum"></param>
        /// <returns></returns>
        List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, int maxNum);

        /// <summary>
        /// 查询列表数据 异步
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查询列表数据 异步
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="maxNum"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, int maxNum);

        /// <summary>
        /// 查询列表数据 同步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<TReturn> GetList<TReturn>(Expression<Func<TEntity, bool>> predicate) where TReturn : class;

        /// <summary>
        /// 查询列表数据 同步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        List<TReturn> GetList<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> expression) where TReturn : class;

        /// <summary>
        /// 查询列表数据 同步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="expression"></param>
        /// <param name="maxNum"></param>
        /// <returns></returns>
        List<TReturn> GetList<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> expression, int maxNum) where TReturn : class;

        /// <summary>
        /// 查询列表数据 异步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<TReturn>> GetListAsync<TReturn>(Expression<Func<TEntity, bool>> predicate) where TReturn : class;

        /// <summary>
        /// 查询列表数据 异步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<List<TReturn>> GetListAsync<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> expression) where TReturn : class;

        /// <summary>
        /// 查询列表数据 异步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="expression"></param>
        /// <param name="maxNum"></param>
        /// <returns></returns>
        Task<List<TReturn>> GetListAsync<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> expression, int maxNum) where TReturn : class;

        /// <summary>
        /// 分页查询 同步
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderExpressions"></param>
        /// <returns></returns>
        IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20, params Expression<Func<TEntity, object>>[] orderExpressions);

        /// <summary>
        /// 分页查询 异步
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderExpressions"></param>
        /// <returns></returns>
        Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20, params Expression<Func<TEntity, object>>[] orderExpressions);

        /// <summary>
        /// 分页查询 同步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderExpressions"></param>
        /// <returns></returns>
        IPagedList<TReturn> GetPagedList<TReturn>(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20, params Expression<Func<TEntity, object>>[] orderExpressions) where TReturn : class;

        /// <summary>
        /// 分页查询 同步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="returnExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderExpressions"></param>
        /// <returns></returns>
        IPagedList<TReturn> GetPagedList<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> returnExpression, int pageIndex = 1, int pageSize = 20, params Expression<Func<TEntity, object>>[] orderExpressions) where TReturn : class;

        /// <summary>
        /// 分页查询 异步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderExpressions"></param>
        /// <returns></returns>
        Task<IPagedList<TReturn>> GetPagedListAsync<TReturn>(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20, params Expression<Func<TEntity, object>>[] orderExpressions) where TReturn : class;

        /// <summary>
        /// 分页查询 异步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="returnExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderExpressions"></param>
        /// <returns></returns>
        Task<IPagedList<TReturn>> GetPagedListAsync<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> returnExpression, int pageIndex = 1, int pageSize = 20, params Expression<Func<TEntity, object>>[] orderExpressions) where TReturn : class;

        #endregion

        #region 数量查询

        /// <summary>
        /// 根据查询条件获取数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        long Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据查询条件获取数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region 最小值查询

        /// <summary>
        /// 根据查询条件和指定列，获取该列的最小值
        /// </summary>
        /// <typeparam name="TResult">最小值的数据类型（如 int、decimal、DateTime 等）</typeparam>
        /// <param name="predicate">查询条件（可选，为 null 时查询所有数据）</param>
        /// <param name="columnExpression">指定要查询最小值的列（如 p => p.Price、p => p.CreateTime）</param>
        /// <returns>指定列的最小值（若无数据返回该类型的默认值，如 int 默认 0、DateTime 默认 1900-01-01）</returns>
        TResult Min<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> columnExpression);

        /// <summary>
        /// 【异步】根据查询条件和指定列，获取该列的最小值
        /// </summary>
        /// <typeparam name="TResult">最小值的数据类型</typeparam>
        /// <param name="predicate">查询条件（可选）</param>
        /// <param name="columnExpression">指定要查询最小值的列</param>
        /// <param name="cancellationToken">取消令牌（可选）</param>
        /// <returns>指定列的最小值（异步任务）</returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> columnExpression, CancellationToken cancellationToken = default);

        #endregion

        #region 最大值查询

        /// <summary>
        /// 根据查询条件和指定列，获取该列的最大值
        /// </summary>
        /// <typeparam name="TResult">最大值的数据类型（如 int、decimal、DateTime 等）</typeparam>
        /// <param name="predicate">查询条件（可选）</param>
        /// <param name="columnExpression">指定要查询最大值的列（如 p => p.Stock、p => p.UpdateTime）</param>
        /// <returns>指定列的最大值（若无数据返回该类型的默认值）</returns>
        TResult Max<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> columnExpression);

        /// <summary>
        /// 【异步】根据查询条件和指定列，获取该列的最大值
        /// </summary>
        /// <typeparam name="TResult">最大值的数据类型</typeparam>
        /// <param name="predicate">查询条件（可选）</param>
        /// <param name="columnExpression">指定要查询最大值的列</param>
        /// <param name="cancellationToken">取消令牌（可选）</param>
        /// <returns>指定列的最大值（异步任务）</returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> columnExpression, CancellationToken cancellationToken = default);

        #endregion
    }

    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGenericRepository<TEntity> : IGenericRepository<TEntity, long>
       where TEntity : class, IEntity<long>, new()
    {

    }
}
