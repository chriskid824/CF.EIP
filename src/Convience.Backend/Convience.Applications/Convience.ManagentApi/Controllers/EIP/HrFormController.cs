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
    }
}
