namespace LS.ERP.Domain.Shared
{
    /// <summary>
    /// 分页接口
    /// </summary>
    public interface IPagedList<T>
    {
        /// <summary>
        /// 页码
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 页容量
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        int TotalPage { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        int TotalCount { get; set; }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        bool HasPreviousPage => PageIndex > 1;

        /// <summary>
        /// 是否有下一页
        /// </summary>
        bool HasNextPage => PageIndex < TotalPage;

        /// <summary>
        /// 数据
        /// </summary>
        List<T> Data { get; set; }
    }
}
