using Microsoft.Extensions.ObjectPool;
using System.Text;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// StringBuilder对象池扩展
    /// </summary>
    public static class StringBuilderPoolExtensions
    {
        /// <summary>
        /// 从对象池中租用StringBuilder对象
        /// </summary>
        /// <param name="pool"></param>
        /// <returns></returns>
        public static PooledStringBuilderWrapper Rent(this ObjectPool<StringBuilder> pool)
        {
            return new PooledStringBuilderWrapper(pool);
        }

        /// <summary>
        /// 从对象池中租用StringBuilder对象，并添加初始内容
        /// </summary>
        public readonly struct PooledStringBuilderWrapper : IDisposable
        {
            /// <summary>
            /// StringBuilder对象池
            /// </summary>
            private readonly ObjectPool<StringBuilder> _pool;

            /// <summary>
            /// StringBuilder对象
            /// </summary>
            public readonly StringBuilder Builder;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="pool"></param>
            public PooledStringBuilderWrapper(ObjectPool<StringBuilder> pool)
            {
                _pool = pool;
                Builder = pool.Get();
            }

            /// <summary>
            /// 释放StringBuilder对象到对象池中
            /// </summary>
            public void Dispose()
            {
                Builder.Clear();
                _pool.Return(Builder);
            }

            /// <summary>
            /// 添加 ToString() 方法，方便获取结果
            /// </summary>
            /// <returns></returns>
            public override string ToString() => Builder.ToString();
        }
    }
}
