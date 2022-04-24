using ITM.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ITM.Service.DataImporter.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RpcController : ControllerBase
    {
        private readonly ILogger<RpcController> _logger;

        public RpcController(ILogger<RpcController> logger)
        {
            _logger = logger;
        }

        [HttpPost("startimport")]
        public IActionResult StartImport(RpcStartImport request)
        {
            IActionResult result = null;

            try
            {


            }
            catch(Exception ex)
            {
                result = StatusCode((int)HttpStatusCode.InternalServerError, new DTO.Error()
                {
                    Message = ex.Message,
                    Code = (int)HttpStatusCode.InternalServerError
                });

            }

            return result;
        }
    }
}