namespace LS.ERP.Domain.Shared
{
    /// <summary>
    /// 所属业务接口
    /// </summary>
    public interface IBusiness
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        long? BusinessId { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        string BusinessName { get; set; }
    }
}
