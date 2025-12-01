using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnceMi.AspNetCore.OSS;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using XW.PrettyLove.Core;

namespace XW.PrettyLove.Web.Front.Controllers
{
    /// <summary>
    /// 文件
    /// </summary>
    public class FileController : BaseController
    {
        private readonly IOSSService ossService;
        private const string defaultBucket = "test";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="factory"></param>
        public FileController(IOSSServiceFactory factory)
        {
            this.ossService = factory.Create("Minio");
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/file/upload")]
        public async Task<FileUploadSingleResult> UploadAsync([FromForm] FileUploadSingleDto formData)
        {
            if (formData == null || formData.Files == null || formData.Files.Count != 1)
                throw new FriendlyException("参数错误", HttpStatusCode.BadRequest);
            using Stream stream = formData.Files[0].OpenReadStream();
            var objectName = $"{DateTime.Now.ToString("yyyy/MM/dd")}/{formData.Files[0].FileName}";
            await ossService.PutObjectAsync(defaultBucket, objectName, stream);
            return new FileUploadSingleResult($"{defaultBucket}/{formData.Files[0].FileName}", objectName);
        }
    }
    public record FileUploadSingleDto(IFormFileCollection Files);
    public record FileUploadSingleResult(string ObjectStoragePath, string ObjectName);
}
