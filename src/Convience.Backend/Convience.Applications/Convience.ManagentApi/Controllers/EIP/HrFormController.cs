using Convience.Model.Models.EIP;
using Convience.Service.EIP;
using Convience.Util.Extension;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Convience.ManagentApi.Controllers.EIP
{
    [Route("api/[controller]")]
    [ApiController]
    public class HrFormController : ControllerBase
    {
        private readonly IHrFormService _hrFormService;
        public HrFormController(IHrFormService hrFormService)
        {
            _hrFormService = hrFormService;
        }
      
        [HttpPost("CheckFormURL")]
        public IActionResult CheckWorkURL(ViewhrInterview data)
        {
            try
            {
                return Ok(_hrFormService.CheckFormURL(data));
            }
            catch (Exception ex)
            {
                return this.BadRequestResult(ex.Message);
            }
        }
        [HttpPost("SendImformation")]
        public IActionResult SendImformation(ViewhrFormImformation data)
        {
            try
            {
                return Ok(_hrFormService.SendImformation(data));
            }
            catch (Exception ex)
            {
                return this.BadRequestResult(ex.Message);
            }
        }
        [HttpPost("GetImformationData")]
        public IActionResult GetWorkData(ViewhrFormImformation data)
        {
            try
            {
                return Ok(_hrFormService.GetImformationData(data));
            }
            catch (Exception ex)
            {
                return this.BadRequestResult(ex.Message);
            }
        }
        [HttpPost("SendWork")]
        public IActionResult SendWork(ViewhrFormWork data)
        {
            try
            {
                return Ok(_hrFormService.SendWork(data));
            }
            catch (Exception ex)
            {
                return this.BadRequestResult(ex.Message);
            }
        }
        [HttpPost("GetWorkData")]
        public IActionResult GetWorkData(ViewhrFormWork data)
        {
            try
            {
                return Ok(_hrFormService.GetWorkData(data));
            }
            catch (Exception ex)
            {
                return this.BadRequestResult(ex.Message);
            }
        }
        [HttpPost("SendConsent")]
        public IActionResult SendConsent(ViewhrFormConsent data)
        {
            try
            {
                return Ok(_hrFormService.SendConsent(data));
            }
            catch (Exception ex)
            {
                return this.BadRequestResult(ex.Message);
            }
        }
        [HttpPost("GetConsentData")]
        public IActionResult GetWorkData(ViewhrFormConsent data)
        {
            try
            {
                return Ok(_hrFormService.GetConsentData(data));
            }
            catch (Exception ex)
            {
                return this.BadRequestResult(ex.Message);
            }
        }
        [HttpPost("PrintImformation")]
        public IActionResult PrintImformation(ViewhrInterview data)
        {
            try
            {
                return Ok(_hrFormService.PrintImformation(data));
            }
            catch (Exception ex)
            {
                return this.BadRequestResult(ex.Message);
            }
        }
        [HttpPost("PrintWork")]
        public IActionResult PrintWork(ViewhrInterview data)
        {
            try
            {
                return Ok(_hrFormService.PrintWork(data));
            }
            catch (Exception ex)
            {
                return this.BadRequestResult(ex.Message);
            }
        }

    }
}
