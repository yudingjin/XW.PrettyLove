using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrettyLove.Core;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PrettyLove.Web.Front.Controllers
{
    /// <summary>
    /// 文件
    /// </summary>
    [AllowAnonymous]
    public class FileController : BaseController
    {
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/file/upload")]
        public async Task<string> UploadAsync([FromForm] FileUploadSingleDto formData)
        {
            if (formData == null || formData.Files == null || formData.Files.Count != 1)
                throw new FriendlyException("参数错误", HttpStatusCode.BadRequest);
            using Stream stream = formData.Files[0].OpenReadStream();
            var objectName = $"upload/{DateTime.Now.ToString("yyyy/MM/dd")}/{formData.Files[0].FileName}";
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, objectName);
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            await System.IO.File.WriteAllBytesAsync(path, FileHelper.StreamToBytes(stream));
            return objectName;
        }
    }
    public record FileUploadSingleDto(IFormFileCollection Files);
}
