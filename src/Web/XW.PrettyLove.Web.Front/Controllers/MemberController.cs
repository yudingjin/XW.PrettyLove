using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using XW.PrettyLove.Application;
using XW.PrettyLove.Core;

namespace XW.PrettyLove.Web.Front.Controllers
{
    /// <summary>
    /// 会员
    /// </summary>
    public class MemberController : BaseController
    {
        private readonly IMemberAppService service;
        private readonly IAspNetUser aspNetUser;
        private readonly JwtOptions options;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        /// <param name="options"></param>
        /// <param name="aspNetUser"></param>
        public MemberController(IMemberAppService service, IOptions<JwtOptions> options, IAspNetUser aspNetUser)
        {
            this.service = service;
            this.options = options.Value;
            this.aspNetUser = aspNetUser;
        }

        /// <summary>
        /// 根据ID获取Member信息
        /// </summary>
        /// <param name="id"></param>
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
        /// 解析手机号码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("/api/member/decryptPhone")]
        [HttpPost]
        [Authorize]
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
