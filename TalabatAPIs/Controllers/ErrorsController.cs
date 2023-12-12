using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatAPIs.Errors;

namespace TalabatAPIs.Controllers
{
    [Route("errors/{Code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(404));
        }
    }
}
