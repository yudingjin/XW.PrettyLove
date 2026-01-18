using FreeSql.DataAnnotations;
using PrettyLove.Domain.Shared;
using PrettyLove.Domain.Wechat;

namespace PrettyLove.Domain
{
    [Table(Name = "sms_message")]
    public class SmsMessage : WechatEntity
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string TemplateId { get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>
        [Column(IsNullable = false, StringLength = 200)]
        public string Content { get; set; }

        /// <summary>
        /// JSON字符串，存储模板参数
        /// </summary>
        [Column(IsNullable = false, StringLength = 200)]
        public string TemplateParams { get; set; }

        /// <summary>
        /// 短信状态
        /// </summary>
        [Column(IsNullable = false)]
        public SmsStatus Status { get; set; } = SmsStatus.Pending;

        /// <summary>
        /// 发送时间
        /// </summary>
        [Column(IsNullable = true)]
        public DateTime? SendTime { get; set; }
    }
}
