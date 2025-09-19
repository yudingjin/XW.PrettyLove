namespace LS.ERP.Domain.Shared
{
    /// <summary>
    /// 多租户接口
    /// </summary>
    public interface ITenant<TKey> where TKey : struct, IEquatable<TKey>
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        TKey? TenantId { get; set; }
    }

    /// <summary>
    /// 多租户接口
    /// </summary>
    public interface ITenant : ITenant<long>
    {

    }
}
