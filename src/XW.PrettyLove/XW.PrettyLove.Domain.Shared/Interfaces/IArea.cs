namespace LS.ERP.Domain.Shared
{
    /// <summary>
    /// 区域接口
    /// </summary>
    public interface IArea
    {
        /// <summary>
        /// 省或直辖市
        /// </summary>
        string ProvinceCode { get; set; }

        /// <summary>
        /// 省或直辖市
        /// </summary>
        string ProvinceName { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        string CityCode { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        string CityName { get; set; }

        /// <summary>
        /// 区或县
        /// </summary>
        string DistrictCode { get; set; }

        /// <summary>
        /// 区或县
        /// </summary>
        string DistrictName { get; set; }
    }
}
