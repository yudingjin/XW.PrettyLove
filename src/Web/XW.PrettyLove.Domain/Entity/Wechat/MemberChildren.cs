using FreeSql.DataAnnotations;
using XW.PrettyLove.Domain.Shared;
using XW.PrettyLove.Domain.Wechat;

namespace XW.PrettyLove.Domain
{
    /// <summary>
    /// 会员子女表
    /// </summary>
    [Table(Name = "member_children")]
    public class MemberChildren : WechatEntity
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        [Column(IsNullable = false)]
        public long? MemberId { get; set; }

        /// <summary>
        /// 名词
        /// </summary>
        [Column(IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// 毕业学校
        /// </summary>
        [Column(IsNullable = false, StringLength = 50)]
        public string School { get; set; } = string.Empty;

        /// <summary>
        /// 出生年份
        /// </summary>
        [Column(IsNullable = true)]
        public DateTime? BirthYear { get; set; }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        [Column(IsNullable = false)]
        public MaritalStatus? MaritalStatus { get; set; }

        /// <summary>
        /// 打算何时结婚
        /// </summary>
        [Column(IsNullable = false)]
        public string WhenMarried { get; set; }

        /// <summary>
        /// 宗教信仰
        /// </summary>
        [Column(IsNullable = false)]
        public Religion? Religion { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        [Column(IsNullable = false, StringLength = 10)]
        public string Nationality { get; set; } = string.Empty;

        /// <summary>
        /// 家乡
        /// </summary>
        [Column(IsNullable = false, StringLength = 100)]
        public string Hometown { get; set; } = string.Empty;

        /// <summary>
        /// 家乡默认索引值
        /// </summary>
        [Column(IsNullable = false, StringLength = 50)]
        public string HometownIndex { get; set; } = string.Empty;

        /// <summary>
        /// 现居住地
        /// </summary>
        [Column(IsNullable = false, StringLength = 200)]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 现居地默认索引值
        /// </summary>
        [Column(IsNullable = false, StringLength = 50)]
        public string AddressIndex { get; set; } = string.Empty;

        /// <summary>
        /// 职业
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string Occupation { get; set; } = string.Empty;

        /// <summary>
        /// 房产情况
        /// </summary>
        [Column(IsNullable = false, StringLength = 100)]
        public string House { get; set; } = string.Empty;

        /// <summary>
        /// 车辆情况
        /// </summary>
        [Column(IsNullable = false, StringLength = 100)]
        public string VehicleInfo { get; set; } = string.Empty;

        /// <summary>
        /// 体型
        /// </summary>
        [Column(IsNullable = false)]
        public BodyType? BodyType { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Column(IsNullable = false)]
        public Gender? Gender { get; set; }

        /// <summary>
        /// 受教育程度
        /// </summary>
        [Column(IsNullable = false)]
        public Education? Education { get; set; }

        /// <summary>
        /// 收入等级
        /// </summary>
        [Column(IsNullable = false)]
        public IncomLevel? IncomLevel { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        [Column(IsNullable = false)]
        public int? Height { get; set; }

        /// <summary>
        /// 默认图片地址
        /// </summary>
        [Column(IsNullable = false)]
        public string DefaultImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 介绍
        /// </summary>
        [Column(IsNullable = false, StringLength = 500)]
        public string Remark { get; set; }
    }
}
