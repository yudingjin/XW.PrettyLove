using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace PrettyLove.Web.Front
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [EnableCors()]
    public abstract class BaseController : ControllerBase
    {

    }
}
