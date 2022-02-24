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
    public class HrInterviewController : ControllerBase
    {
        private readonly IHrInterviewService _hrInterviewService;
        public HrInterviewController(IHrInterviewService hrInterviewService)
        //IOptions<appSettings> appSettingsOption,
        //ISrmSupplierService srmSupplierService)
        {
            _hrInterviewService = hrInterviewService;
            //_appSettingsService = appSettingsOption.Value;
            //_srmSupplierService = srmSupplierService;
        }
        [HttpPost("GetInterviewList")]
        public IActionResult GetInterviewList(QueryInterview query)
        {
            return Ok(_hrInterviewService.GetInterviewList(query));
        }
        [HttpPost("GetInterviewDetail")]
        public IActionResult GetInterviewDetail(QueryInterview query)
        {
            var detail = _hrInterviewService.GetInterviewDetail(query);
            return Ok(detail);
        }
        [HttpPost("Update")]
        public IActionResult UpdateInterview(ViewhrInterview data)
        {
            try
            {
                _hrInterviewService.UpdateInterview(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequestResult(ex.Message);
            }
        }
        [HttpPost("DeleteList")]
        public IActionResult DeleteList(ViewhrInterview data)
        {
            try
            {
                return Ok(_hrInterviewService.DeleteList(data));
            }
            catch (Exception ex)
            {
                return this.BadRequestResult(ex.Message);
            }
        }
        [HttpPost("AddList")]
        public IActionResult AddList(ViewhrInterview data)
        {
            try
            {
                return Ok(_hrInterviewService.AddList(data));
            }
            catch (Exception ex)
            {
                return this.BadRequestResult(ex.Message);
            }
        }
    }
}
