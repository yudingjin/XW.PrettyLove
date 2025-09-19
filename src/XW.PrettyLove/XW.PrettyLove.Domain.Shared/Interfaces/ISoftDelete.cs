namespace LS.ERP.Domain.Shared
{
    /// <summary>
    /// 逻辑删除接口
    /// </summary>
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
