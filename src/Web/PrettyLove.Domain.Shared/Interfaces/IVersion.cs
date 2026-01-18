namespace PrettyLove.Domain.Shared
{
    /// <summary>
    /// 乐观锁
    /// </summary>
    public interface IVersion<TKey> where TKey : struct, IEquatable<TKey>
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        TKey Version { get; set; }
    }

    /// <summary>
    /// 乐观锁 版本号
    /// </summary>
    public interface IVersion : IVersion<int>
    {

    }
}
