using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using Rougamo.Context;
using System.Data;
using PrettyLove.Domain.Shared;

namespace PrettyLove.Core
{
    /// <summary>
    /// 仓储事务特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TransactionalAttribute : Rougamo.MoAttribute
    {
        /// <summary>
        /// 事务传播方式
        /// </summary>
        public Propagation Propagation { get; set; } = Propagation.Required;

        IsolationLevel? m_IsolationLevel = IsolationLevel.ReadCommitted;
        /// <summary>
        /// 事务级别
        /// </summary>
        public IsolationLevel IsolationLevel { get => m_IsolationLevel.Value; set => m_IsolationLevel = value; }

        /// <summary>
        /// 当前库
        /// </summary>
        readonly DBEnum currentDb;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="db"></param>
        public TransactionalAttribute(DBEnum db)
        {
            currentDb = db;
        }

        /// <summary>
        /// 服务提供者
        /// </summary>
        static AsyncLocal<IServiceProvider> m_ServiceProvider = new AsyncLocal<IServiceProvider>();
        public static void SetServiceProvider(IServiceProvider serviceProvider) => m_ServiceProvider.Value = serviceProvider;

        IUnitOfWork _uow;
        public override void OnEntry(MethodContext context)
        {
            var uowManager = m_ServiceProvider.Value.GetService<UnitOfWorkManagerCloud>();
            _uow = uowManager.Begin(currentDb, this.Propagation, this.m_IsolationLevel);
        }

        /// <summary>
        /// 事务提交或回滚
        /// </summary>
        /// <param name="context"></param>
        public override void OnExit(MethodContext context)
        {
            if (typeof(Task).IsAssignableFrom(context.ReturnType))
                ((Task)context.ReturnValue).ContinueWith(t => _OnExit());
            else _OnExit();

            void _OnExit()
            {
                try
                {
                    if (context.Exception == null) _uow.Commit();
                    else _uow.Rollback();
                }
                finally
                {
                    _uow.Dispose();
                }
            }
        }
    }
}
