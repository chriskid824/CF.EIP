using Convience.Model.Models.EIP;
using Convience.Service.EIP;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Convience.ManagentApi.Controllers.EIP
{
    [Route("api/[controller]")]
    [ApiController]
    public class HrCandidateController : ControllerBase
    {
        private readonly IHrCandidateService _hrCandidateService;
        //private readonly appSettings _appSettingsService;


        public HrCandidateController(IHrCandidateService hrCandidateService)
            //IOptions<appSettings> appSettingsOption,
            //ISrmSupplierService srmSupplierService)
        {
            _hrCandidateService = hrCandidateService;
            //_appSettingsService = appSettingsOption.Value;
            //_srmSupplierService = srmSupplierService;
        }
        [HttpPost("GetCandidatelList")]
        public IActionResult GetCandidatelList(QueryCandidate query)
        {
            return Ok(_hrCandidateService.GetCandidatelList(query));
        }
    }
}
