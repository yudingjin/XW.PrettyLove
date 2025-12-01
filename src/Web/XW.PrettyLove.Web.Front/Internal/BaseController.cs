using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace XW.PrettyLove.Web.Front
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [EnableCors()]
    public class BaseController : ControllerBase
    {

    }
}
