using FreeSql;
using PrettyLove.Domain;
using PrettyLove.Domain.Shared;
using System.Linq.Expressions;

namespace PrettyLove.Core
{
    /// <summary>
    /// 泛型仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> : GenericRepository<TEntity, long>, IGenericRepository<TEntity>
        where TEntity : class, IEntity<long>, new()
    {
        public GenericRepository(AppCloud cloud, UnitOfWorkManagerCloud managerCloud)
            : base(cloud, managerCloud)
        {

        }
    }

    /// <summary>
    /// 泛型仓储接口实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TKey : struct, IEquatable<TKey>
    {
        #region 成员变量和构造

        /// <summary>
        /// 线程锁
        /// </summary>
        static object objLock = new object();

        /// <summary>
        /// FreeSqlCloud
        /// </summary>
        protected AppCloud Cloud { get; private set; }

        /// <summary>
        /// FreeSql
        /// </summary>
        public IFreeSql Client { get; private set; }

        DbContext currentDbContext;
        /// <summary>
        /// 数据库上下文
        /// </summary>
        public DbContext CurrentDbContext
        {
            get
            {
                lock (objLock)
                {
                    if (currentDbContext == null)
                    {
                        lock (objLock)
                        {
                            currentDbContext = Client.CreateDbContext();
                        }
                    }
                    return currentDbContext;
                }
            }
        }

        /// <summary>
        /// 静态构造
        /// </summary>
        static GenericRepository()
        {
            DBCache.Add(typeof(TEntity), Activator.CreateInstance<TEntity>().DBEnum);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cloud"></param>
        /// <param name="managerCloud"></param>
        public GenericRepository(AppCloud cloud, UnitOfWorkManagerCloud managerCloud)
        {
            this.Cloud = cloud;
            this.Client = cloud.Use(DBCache.GetDb(typeof(TEntity)));
            managerCloud.GetUnitOfWorkManager(DBCache.GetDb(typeof(TEntity)));
        }

        #endregion

        #region 数据库与工作单元

        /// <summary>
        /// 数据库切换
        /// </summary>
        /// <param name="dbEnum"></param>
        /// <returns></returns>
        public virtual void SwitchDB(DBEnum dbEnum)
        {
            Client = Cloud.Use(dbEnum);
        }

        /// <summary>
        /// 手动提交  作用于DbContext
        /// 同步方法
        /// </summary>
        /// <returns></returns>
        public virtual int SaveChanges()
        {
            int result = CurrentDbContext.SaveChanges();
            CurrentDbContext?.Dispose();
            return result;
        }

        /// <summary>
        /// 手机提交  作用于DbContext
        /// 异步方法
        /// </summary>
        /// <returns></returns>
        public virtual Task<int> SaveChangesAsync()
        {
            var result = CurrentDbContext.SaveChangesAsync();
            CurrentDbContext?.Dispose();
            return result;
        }

        /// <summary>
        /// 开启同线程事务
        /// 不支持异步
        /// </summary>
        /// <param name="action"></param>
        public virtual void Transaction(Action action)
        {
            Client.Transaction(action);
        }

        /// <summary>
        /// 工作单元事务提交
        /// 支持异步
        /// </summary>
        /// <returns></returns>
        public virtual void Transaction(Action<IFreeSql> action)
        {
            using (var uow = Client.CreateUnitOfWork())
            {
                action?.Invoke(Client);
                uow.Commit();
            }
        }

        #endregion

        #region 新增数据

        /// <summary>
        /// DbContext 新增数据
        /// 同步方法
        /// </summary>
        /// <param name="data"></param>
        public virtual void Add(TEntity data)
        {
            CurrentDbContext.Add(data);
        }

        /// <summary>
        /// DbContext 新增数据
        /// 异步方法
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task AddAsync(TEntity data, CancellationToken cancellationToken = default)
        {
            return CurrentDbContext.AddAsync(data, cancellationToken);
        }

        /// <summary>
        /// DbContext 新增数据
        /// 同步方法
        /// </summary>
        /// <param name="data"></param>
        public virtual void AddRange(IEnumerable<TEntity> data)
        {
            CurrentDbContext.AddRange(data);
        }

        /// <summary>
        /// DbContext 新增数据
        /// 异步方法
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task AddRangeAsync(IEnumerable<TEntity> data, CancellationToken cancellationToken = default)
        {
            return CurrentDbContext.AddRangeAsync(data, cancellationToken);
        }

        /// <summary>
        /// 新增一条数据 同步
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ignoreColumns"></param>
        /// <returns></returns>
        public virtual int Insert(TEntity entity, params string[] ignoreColumns)
        {
            var insertEntity = Client.Insert(entity);
            if (ignoreColumns?.Length > 0)
                insertEntity.IgnoreColumns(ignoreColumns);
            return insertEntity.ExecuteAffrows();
        }

        /// <summary>
        /// 新增一条数据 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="ignoreColumns"></param>
        /// <returns></returns>
        public virtual Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default, params string[] ignoreColumns)
        {
            var insertEntity = Client.Insert(entity);
            if (ignoreColumns?.Length > 0)
                insertEntity.IgnoreColumns(ignoreColumns);
            return insertEntity.ExecuteAffrowsAsync(cancellationToken);
        }

        /// <summary>
        /// 新增多条数据 同步
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="ignoreColumns"></param>
        /// <returns></returns>
        public virtual int Insert(IEnumerable<TEntity> entities, params string[] ignoreColumns)
        {
            var insertEntity = Client.Insert(entities);
            if (ignoreColumns?.Length > 0)
                insertEntity.IgnoreColumns(ignoreColumns);
            return insertEntity.ExecuteAffrows();
        }

        /// <summary>
        /// 新增多条数据 异步
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="ignoreColumns"></param>
        /// <returns></returns>
        public virtual Task<int> InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default, params string[] ignoreColumns)
        {
            var insertEntity = Client.Insert(entities);
            if (ignoreColumns?.Length > 0)
                insertEntity.IgnoreColumns(ignoreColumns);
            return insertEntity.ExecuteAffrowsAsync(cancellationToken);
        }

        /// <summary>
        /// 插入或者新增数据
        /// </summary>
        /// <param name="entity"></param>
        public virtual void AddOrUpdate(TEntity entity)
        {
            CurrentDbContext.AddOrUpdate(entity);
        }

        #endregion

        #region 修改数据

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <returns></returns>
        public IUpdate<TEntity> UpdateEntity()
        {
            return Client.Update<TEntity>();
        }

        /// <summary>
        /// DbContext 修改数据
        /// 同步方法
        /// </summary>
        /// <param name="data"></param>
        public virtual void Update(TEntity data)
        {
            CurrentDbContext.Update(data);
        }

        /// <summary>
        /// DbContext 修改数据
        /// 异步方法
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task UpdateAsync(TEntity data, CancellationToken cancellationToken = default)
        {
            return CurrentDbContext.UpdateAsync(data, cancellationToken);
        }

        /// <summary>
        /// DbContext 修改数据
        /// 同步方法
        /// </summary>
        /// <param name="data"></param>
        public virtual void UpdateRange(IEnumerable<TEntity> data)
        {
            CurrentDbContext.UpdateRange(data);
        }

        /// <summary>
        /// DbContext 修改数据
        /// 异步方法
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task UpdateRangeAsync(IEnumerable<TEntity> data, CancellationToken cancellationToken = default)
        {
            return CurrentDbContext.UpdateRangeAsync(data, cancellationToken);
        }

        /// <summary>
        /// 修改数据 同步
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updateColumns"></param>
        /// <returns></returns>
        public virtual int Update(TEntity entity, string[] updateColumns)
        {
            var updateEntity = UpdateEntity().SetSource(entity);
            if (updateColumns?.Length > 0)
                updateEntity.UpdateColumns(updateColumns);
            return updateEntity.ExecuteAffrows();
        }

        /// <summary>
        /// 修改数据 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updateColumns"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> UpdateAsync(TEntity entity, string[] updateColumns, CancellationToken cancellationToken = default)
        {
            var updateEntity = UpdateEntity().SetSource(entity);
            if (updateColumns?.Length > 0)
                updateEntity.UpdateColumns(updateColumns);
            return updateEntity.ExecuteAffrowsAsync(cancellationToken);
        }

        /// <summary>
        /// 修改数据 同步
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="updateColumns"></param>
        /// <returns></returns>
        public virtual int Update(IEnumerable<TEntity> entities, string[] updateColumns)
        {
            var updateEntity = UpdateEntity().SetSource(entities);
            if (updateColumns?.Length > 0)
                updateEntity.UpdateColumns(updateColumns);
            var result = updateEntity.ExecuteAffrows();
            return result;
        }

        /// <summary>
        /// 修改数据 异步
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="updateColumns"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> UpdateAsync(IEnumerable<TEntity> entities, string[] updateColumns, CancellationToken cancellationToken = default)
        {
            var updateEntity = UpdateEntity().SetSource(entities);
            if (updateColumns?.Length > 0)
                updateEntity.UpdateColumns(updateColumns);
            var result = updateEntity.ExecuteAffrowsAsync(cancellationToken);
            return result;
        }

        /// <summary>
        /// 修改数据 同步
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual int Modify(TEntity entity, Expression<Func<TEntity, object>> expression = null)
        {
            //实例.UpdateColumns(a => new { a.Title, a.CreateTime })
            var updateEntity = UpdateEntity().SetSource(entity);
            if (expression != null)
                updateEntity.UpdateColumns(expression);
            return updateEntity.ExecuteAffrows();
        }

        /// <summary>
        /// 修改数据 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> ModifyAsync(TEntity entity, Expression<Func<TEntity, object>> expression = null, CancellationToken cancellationToken = default)
        {
            //实例.UpdateColumns(a => new { a.Title, a.CreateTime })
            var updateEntity = UpdateEntity().SetSource(entity);
            if (expression != null)
                updateEntity.UpdateColumns(expression);
            return updateEntity.ExecuteAffrowsAsync(cancellationToken);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual int Modify(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> expression = null)
        {
            //实例.UpdateColumns(a => new { a.Title, a.CreateTime })
            var updateEntity = UpdateEntity().SetSource(entities);
            if (expression != null)
                updateEntity.UpdateColumns(expression);
            var result = updateEntity.ExecuteAffrows();
            return result;
        }

        /// <summary>
        /// 修改数据 异步
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> ModifyAsync(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> expression = null, CancellationToken cancellationToken = default)
        {
            //实例.UpdateColumns(a => new { a.Title, a.CreateTime })
            var updateEntity = UpdateEntity().SetSource(entities);
            if (expression != null)
                updateEntity.UpdateColumns(expression);
            var result = updateEntity.ExecuteAffrowsAsync(cancellationToken);
            return result;
        }

        #endregion

        #region 逻辑删除

        /// <summary>
        /// 逻辑删除 同步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int FakeDelete<Entity>(Entity entity) where Entity : class, IEntity<long>, ISoftDelete
        {
            return Client.Update<Entity>().Set(p => p.IsDeleted == true).Where(p => p.Id == entity.Id).ExecuteAffrows();
        }

        /// <summary>
        /// 逻辑删除 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<int> FakeDeleteAsync<Entity>(Entity entity) where Entity : class, IEntity<long>, ISoftDelete
        {
            return Client.Update<Entity>().Set(p => p.IsDeleted == true).Where(p => p.Id == entity.Id).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 批量逻辑删除 同步
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int FakeDelete<Entity>(IEnumerable<Entity> entities) where Entity : class, IEntity<long>, ISoftDelete
        {
            var idList = entities.Select(p => p.Id).ToList();
            return Client.Update<Entity>().Set(p => p.IsDeleted == true).Where(p => idList.Contains(p.Id)).ExecuteAffrows();
        }

        /// <summary>
        /// 批量逻辑删除 异步
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public Task<int> FakeDeleteAsync<Entity>(IEnumerable<Entity> entities) where Entity : class, IEntity<long>, ISoftDelete
        {
            var idList = entities.Select(p => p.Id).ToList();
            return Client.Update<Entity>().Set(p => p.IsDeleted == true).Where(p => idList.Contains(p.Id)).ExecuteAffrowsAsync();
        }

        #endregion

        #region 物理删除

        /// <summary>
        /// DbContext 删除数据
        /// </summary>
        /// <param name="data"></param>
        public virtual void Remove(TEntity data)
        {
            CurrentDbContext.Remove(data);
        }

        /// <summary>
        /// DbContext 删除数据
        /// </summary>
        /// <param name="data"></param>
        public virtual void RemoveRange(IEnumerable<TEntity> data)
        {
            CurrentDbContext.RemoveRange(data);
        }

        /// <summary>
        /// 删除数据 同步
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Delete(IEnumerable<TEntity> entities)
        {
            var result = Client.Delete<TEntity>(entities).ExecuteAffrows();
            return result;
        }

        /// <summary>
        /// 删除数据 异步
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<int> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            var result = Client.Delete<TEntity>(entities).ExecuteAffrowsAsync(cancellationToken);
            return result;
        }

        /// <summary>
        /// 删除数据 同步
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var result = Client.GetRepository<TEntity>().Delete(predicate);
            return result;
        }

        /// <summary>
        /// 删除数据 异步
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var result = Client.GetRepository<TEntity>().DeleteAsync(predicate, cancellationToken);
            return result;
        }

        /// <summary>
        /// 删除数据 同步
        /// </summary>
        /// <param name="dywhere"></param>
        /// <returns></returns>
        public int Delete(object dywhere)
        {
            var result = Client.Delete<TEntity>(dywhere).ExecuteAffrows();
            return result;
        }

        /// <summary>
        /// 删除数据 异步
        /// </summary>
        /// <param name="dywhere"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<int> DeleteAsync(object dywhere, CancellationToken cancellationToken = default)
        {
            var result = Client.Delete<TEntity>(dywhere).ExecuteAffrowsAsync(cancellationToken);
            return result;
        }

        #endregion

        #region 查询数据

        /// <summary>
        /// ISelect构造
        /// </summary>
        /// <returns></returns>
        public ISelect<TEntity> Select()
        {
            return Client.Select<TEntity>();
        }

        /// <summary>
        /// ISelect构造
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISelect<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            if (expression == null)
                return Select();
            return Select().Where(expression);
        }

        /// <summary>
        /// 根据主键查询数据 同步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="id"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TReturn Find<TReturn>(TKey id, Expression<Func<TEntity, TReturn>> expression) where TReturn : class
        {
            var query = Where(p => p.Id.Equals(id));
            return query.First(expression);
        }

        /// <summary>
        /// 根据主键查询数据 异步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="id"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<TReturn> FindAsync<TReturn>(TKey id, Expression<Func<TEntity, TReturn>> expression) where TReturn : class
        {
            var query = Where(p => p.Id.Equals(id));
            return query.FirstAsync(expression);
        }

        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetByKey(TKey id)
        {
            return Where(p => p.Id.Equals(id)).ToOne();
        }

        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<TEntity> GetByKeyAsync(TKey id)
        {
            return Where(p => p.Id.Equals(id)).ToOneAsync();
        }

        /// <summary>
        /// 查询单条数据 同步
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Where(predicate).ToOne();
        }

        /// <summary>
        /// 查询单条数据 异步
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Where(predicate).ToOneAsync();
        }

        /// <summary>
        /// 查询单条数据 同步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TReturn Get<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> expression) where TReturn : class
        {
            return Where(predicate).ToOne(expression);
        }

        /// <summary>
        /// 查询单条数据 异步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<TReturn> GetAsync<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> expression) where TReturn : class
        {
            return Where(predicate).ToOneAsync(expression);
        }

        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return Select().Any(predicate);
        }

        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Select().AnyAsync(predicate);
        }

        /// <summary>
        /// 查询列表数据 同步
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return Where(predicate).ToList();
        }

        /// <summary>
        /// 查询列表数据 同步
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="maxNum"></param>
        /// <returns></returns>
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, int maxNum)
        {
            return Where(predicate).Take(maxNum).ToList();
        }

        /// <summary>
        /// 查询列表数据 异步
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Where(predicate).ToListAsync();
        }

        /// <summary>
        /// 查询列表数据 异步
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="maxNum"></param>
        /// <returns></returns>
        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, int maxNum)
        {
            return Where(predicate).Take(maxNum).ToListAsync();
        }

        /// <summary>
        /// 查询列表数据 同步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public List<TReturn> GetList<TReturn>(Expression<Func<TEntity, bool>> predicate) where TReturn : class
        {
            return Where(predicate).ToList<TReturn>();
        }

        /// <summary>
        /// 查询列表数据 同步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public List<TReturn> GetList<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> expression) where TReturn : class
        {
            return Where(predicate).ToList(expression);
        }

        /// <summary>
        /// 查询列表数据 同步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public List<TReturn> GetList<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> expression, int maxNum) where TReturn : class
        {
            return Where(predicate).Take(maxNum).ToList(expression);
        }

        /// <summary>
        /// 查询列表数据 异步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<List<TReturn>> GetListAsync<TReturn>(Expression<Func<TEntity, bool>> predicate) where TReturn : class
        {
            return Where(predicate).ToListAsync<TReturn>();
        }

        /// <summary>
        /// 查询列表数据 异步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<List<TReturn>> GetListAsync<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> expression) where TReturn : class
        {
            return Where(predicate).ToListAsync(expression);
        }

        /// <summary>
        /// 查询列表数据 异步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<List<TReturn>> GetListAsync<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> expression, int maxNum) where TReturn : class
        {
            return Where(predicate).Take(maxNum).ToListAsync(expression);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderExpressions"></param>
        /// <returns></returns>
        public IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20, params Expression<Func<TEntity, object>>[] orderExpressions)
        {
            var query = Where(predicate);
            var total = query.Count();
            if (orderExpressions?.Length > 0)
            {
                orderExpressions.ToList().ForEach(p =>
                {
                    query.OrderByDescending(p);
                });
            }
            else
            {
                query.OrderByDescending(p => p.Id);
            }
            var list = query.Page(pageIndex, pageSize).ToList();
            return new PagedList<TEntity>(total, pageSize, list);
        }

        /// <summary>
        /// 分页查询 异步
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderExpressions"></param>
        /// <returns></returns>
        public async Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20, params Expression<Func<TEntity, object>>[] orderExpressions)
        {
            var query = Where(predicate);
            var total = await query.CountAsync();
            if (orderExpressions?.Length > 0)
            {
                orderExpressions.ToList().ForEach(p =>
                {
                    query.OrderByDescending(p);
                });
            }
            var list = await query.Page(pageIndex, pageSize).ToListAsync();
            return new PagedList<TEntity>(total, pageSize, list);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderExpressions"></param>
        /// <returns></returns>
        public IPagedList<TReturn> GetPagedList<TReturn>(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20, params Expression<Func<TEntity, object>>[] orderExpressions) where TReturn : class
        {
            var query = Where(predicate);
            var total = query.Count();
            if (orderExpressions?.Length > 0)
            {
                orderExpressions.ToList().ForEach(p =>
                {
                    query.OrderByDescending(p);
                });
            }
            var list = query.Page(pageIndex, pageSize).ToList<TReturn>();
            return new PagedList<TReturn>(total, pageSize, list);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="returnExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderExpressions"></param>
        /// <returns></returns>
        public IPagedList<TReturn> GetPagedList<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> returnExpression, int pageIndex = 1, int pageSize = 20, params Expression<Func<TEntity, object>>[] orderExpressions) where TReturn : class
        {
            var query = Where(predicate);
            var total = query.Count();
            if (orderExpressions?.Length > 0)
            {
                orderExpressions.ToList().ForEach(p =>
                {
                    query.OrderByDescending(p);
                });
            }
            var list = query.Page(pageIndex, pageSize).ToList(returnExpression);
            return new PagedList<TReturn>(total, pageSize, list);
        }

        /// <summary>
        /// 分页查询 异步
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderExpressions"></param>
        /// <returns></returns>
        public async Task<IPagedList<TReturn>> GetPagedListAsync<TReturn>(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20, params Expression<Func<TEntity, object>>[] orderExpressions) where TReturn : class
        {
            var query = Where(predicate);
            var total = await query.CountAsync();
            if (orderExpressions?.Length > 0)
            {
                orderExpressions.ToList().ForEach(p =>
                {
                    query.OrderByDescending(p);
                });
            }
            var list = await query.Page(pageIndex, pageSize).ToListAsync<TReturn>();
            return new PagedList<TReturn>(total, pageSize, list);
        }

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
        public async Task<IPagedList<TReturn>> GetPagedListAsync<TReturn>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TReturn>> returnExpression, int pageIndex = 1, int pageSize = 20, params Expression<Func<TEntity, object>>[] orderExpressions) where TReturn : class
        {
            var query = Where(predicate);
            var total = await query.CountAsync();
            if (orderExpressions?.Length > 0)
            {
                orderExpressions.ToList().ForEach(p =>
                {
                    query.OrderByDescending(p);
                });
            }
            var list = await query.Page(pageIndex, pageSize).ToListAsync(returnExpression);
            return new PagedList<TReturn>(total, pageSize, list);
        }

        #endregion

        #region 数量查询

        /// <summary>
        /// 根据查询条件获取数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public long Count(Expression<Func<TEntity, bool>> predicate)
        {
            var number = Where(predicate).Count();
            return number;
        }

        /// <summary>
        /// 根据查询条件获取数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var number = Where(predicate).CountAsync();
            return number;
        }

        #endregion

        #region 最小值查询

        /// <summary>
        /// 根据查询条件获取最小值
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TResult Min<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> columnExpression)
        {
            // 1. 构建查询：若有条件则附加 Where，否则查询所有
            var query = predicate == null ? Select() : Where(predicate);

            // 2. 提取指定列并查询最小值（FreeSql 的 Min 方法会自动处理空数据场景）
            return query.Min(columnExpression);
        }

        /// <summary>
        /// 根据查询条件获取最小值
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> columnExpression, CancellationToken cancellationToken = default)
        {
            var query = predicate == null ? Select() : Where(predicate);
            // 调用 FreeSql 异步 Min 方法，支持取消令牌
            return await query.MinAsync(columnExpression, cancellationToken);
        }

        #endregion

        #region 最大值查询

        /// <summary>
        /// 根据查询条件和指定列，获取该列的最大值
        /// </summary>
        /// <typeparam name="TResult">最大值的数据类型（如 int、decimal、DateTime 等）</typeparam>
        /// <param name="predicate">查询条件（可选）</param>
        /// <param name="columnExpression">指定要查询最大值的列（如 p => p.Stock、p => p.UpdateTime）</param>
        /// <returns>指定列的最大值（若无数据返回该类型的默认值）</returns>
        public TResult Max<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> columnExpression)
        {
            var query = predicate == null ? Select() : Where(predicate);
            // FreeSql 的 Max 方法自动处理空数据
            return query.Max(columnExpression);
        }

        /// <summary>
        /// 【异步】根据查询条件和指定列，获取该列的最大值
        /// </summary>
        /// <typeparam name="TResult">最大值的数据类型</typeparam>
        /// <param name="predicate">查询条件（可选）</param>
        /// <param name="columnExpression">指定要查询最大值的列</param>
        /// <param name="cancellationToken">取消令牌（可选）</param>
        /// <returns>指定列的最大值（异步任务）</returns>
        public async Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> columnExpression, CancellationToken cancellationToken = default)
        {
            var query = predicate == null ? Select() : Where(predicate);
            return await query.MaxAsync(columnExpression, cancellationToken);
        }

        #endregion
    }
}
