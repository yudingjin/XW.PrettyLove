using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrettyLove.Application;
using PrettyLove.Core;
using PrettyLove.Domain;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace PrettyLove.Web.Front.Controllers
{
    /// <summary>
    /// 会员
    /// </summary>
    public class MemberController : BaseController
    {
        private readonly IMemberAppService service;
        private readonly IAspNetUser aspNetUser;
        private readonly IGenericRepository<MemberSuggestion> suggestionRepository;
        private readonly JwtOptions options;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        /// <param name="aspNetUser"></param>
        /// <param name="suggestionRepository"></param>
        /// <param name="options"></param>
        public MemberController
        (
            IMemberAppService service,
            IAspNetUser aspNetUser,
            IGenericRepository<MemberSuggestion> suggestionRepository,
            IOptions<JwtOptions> options
        )
        {
            this.service = service;
            this.options = options.Value;
            this.aspNetUser = aspNetUser;
            this.suggestionRepository = suggestionRepository;
        }

        /// <summary>
        /// 根据ID获取Member信息
        /// </summary>
        /// <param name="id">会员ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/member/get")]
        [AllowAnonymous]
        public async Task<WechatLoginResponseDTO> GetAsync(long? id)
        {
            if (id == null)
                throw new FriendlyException("参数错误", HttpStatusCode.BadRequest);

            var memberInfo = await service.GetByKeyAsync(id.Value);
            if (memberInfo == null)
                throw new FriendlyException("登录失败", HttpStatusCode.InternalServerError);
            return new WechatLoginResponseDTO
            {
                OpenId = memberInfo.OpenId,
                SessionKey = memberInfo.SessionKey,
                UserId = memberInfo.Id,
                UserName = memberInfo.NickName,
                Token = memberInfo.GetAccessToken(options)
            };
        }

        /// <summary>
        /// 提交用户反馈
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/suggestion")]
        public async Task<int> SubmitSuggestion([FromBody] MemberSuggestionFormDTO form)
        {
            if (form == null)
                throw new FriendlyException("参数不能为空", HttpStatusCode.BadRequest);
            if (form.Content.Length > 200)
                throw new FriendlyException("建议内容不能超过200字", HttpStatusCode.BadRequest);
            var suggestion = new MemberSuggestion
            {
                Id = YitIdHelper.NextId(),
                Content = form.Content,
                MemberId = aspNetUser?.ID ?? 0,
                CreatedTime = DateTime.Now,
                Status = 0
            };
            return await suggestionRepository.InsertAsync(suggestion);
        }

        /// <summary>
        /// 解析手机号码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("/api/member/decryptPhone")]
        [HttpPost]
        public async Task<string> DecryptPhoneNumber(DecryptDTO dto)
        {
            if (string.IsNullOrEmpty(dto.EncryptedData) || string.IsNullOrEmpty(dto.SessionKey) || string.IsNullOrEmpty(dto.Iv))
            {
                throw new FriendlyException("参数错误", HttpStatusCode.BadRequest);
            }
            var encryptedDataBytes = Convert.FromBase64String(dto.EncryptedData);
            var sessionKeyBytes = Convert.FromBase64String(dto.SessionKey);
            var ivBytes = Convert.FromBase64String(dto.Iv);
            using (var aes = Aes.Create())
            {
                aes.Key = sessionKeyBytes;
                aes.IV = ivBytes;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                using (var decryptor = aes.CreateDecryptor())
                {
                    var decryptedData = decryptor.TransformFinalBlock(encryptedDataBytes, 0, encryptedDataBytes.Length);
                    var decryptedString = Encoding.UTF8.GetString(decryptedData);
                    var result = JsonConvert.DeserializeObject<JObject>(decryptedString);
                    var phoneNumber = result["phoneNumber"].ToString();
                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        var userInfo = await service.GetByKeyAsync(aspNetUser.ID);
                        if (userInfo != null)
                        {
                            userInfo.Phone = phoneNumber;
                            await service.UpdatePhoneAsync(userInfo);
                        }
                    }
                    return phoneNumber;
                }
            }
        }
    }
}
