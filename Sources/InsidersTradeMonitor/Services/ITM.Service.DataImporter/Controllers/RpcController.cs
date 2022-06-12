using ITM.DTO;
using ITM.Interfaces;
using ITM.Service.DataImporter.Helpers;
using ITM.Service.DataImporter.Workers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ITM.Service.DataImporter.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RpcController : ControllerBase
    {
        private readonly ILogger<RpcController> _logger;
        private readonly IForm4DalWrapper _form4DalWrapper;
        private readonly IForm4ImportersRespository _importersRepo;

        public RpcController(ILogger<RpcController> logger,
                                IForm4DalWrapper form4DalWrapper,
                                IForm4ImportersRespository importersRepo)
        {
            _logger = logger;
            _form4DalWrapper = form4DalWrapper;
            _importersRepo = importersRepo;
        }

        [HttpPost("startimport")]
        public IActionResult StartImport(RpcStartImport request)
        {
            IActionResult result = null;

            try
            {
                var parser = new ITM.Parser.Form4.Form4Parser();

                var source = new ITM.Source.SEC.SECSource();
                ISourceInitParams sourceInitParams = source.CreateInitParams();
                sourceInitParams.Logger = new ITM.Logging.NullLogger();

                var impParams = new Form4ImporterParams()
                {
                    CIK = request.CIK,
                    DateFrom = request.DateFrom,
                    DateTo = request.DateTo,
                    FilingParser = parser,
                    Form4DalWrappwer = _form4DalWrapper,
                    Source = source
                };

                var importer = new Form4Importer(impParams);
                if(importer.Start())
                {
                    var respBody = new RpcStartImportResponse()
                    {
                        ProcessID = importer.ProcessID
                    };

                    result = StatusCode((int)HttpStatusCode.Created, respBody);
                }
                else
                {
                    throw new Exception("Failed to start Form4Importer");
                }
            }
            catch (Exception ex)
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