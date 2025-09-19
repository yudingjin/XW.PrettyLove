using Microsoft.AspNetCore.Mvc;

namespace XW.PrettyLove.Web.Admin.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("success");
    }
}
