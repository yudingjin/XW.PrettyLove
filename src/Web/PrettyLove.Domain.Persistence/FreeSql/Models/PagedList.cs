using PrettyLove.Domain.Shared;

namespace PrettyLove.Core
{
    /// <summary>
    /// 分页类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : IPagedList<T>
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage => PageIndex > 1;

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage => PageIndex < TotalPage;

        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="data"></param>
        public PagedList(long totalCount, List<T> data)
        {
            TotalCount = Convert.ToInt32(totalCount);
            TotalPage = (Convert.ToInt32(totalCount) + PageSize - 1) / PageSize;
            Data = data;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageSize"></param>
        /// <param name="data"></param>
        public PagedList(long totalCount, int pageSize, List<T> data)
        {
            PageSize = pageSize;
            TotalCount = Convert.ToInt32(totalCount);
            TotalPage = (Convert.ToInt32(totalCount) + pageSize - 1) / pageSize;
            Data = data;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="data"></param>
        /// <param name="totalCount"></param>
        public PagedList(int pageIndex, int pageSize, List<T> data, long totalCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = Convert.ToInt32(totalCount);
            TotalPage = (Convert.ToInt32(totalCount) + pageSize - 1) / pageSize;
            Data = data;
        }
    }
}
