using Microsoft.AspNetCore.Mvc;
using PublicApi.MiddleWare;

namespace PublicApi.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseAPIController : ControllerBase
    {
    }
}
