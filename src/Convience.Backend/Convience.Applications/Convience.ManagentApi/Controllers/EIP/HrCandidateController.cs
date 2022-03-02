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
    public class HrCandidateController : ControllerBase
    {
        private readonly IHrCandidateService _hrCandidateService;
        //private readonly appSettings _appSettingsService;


        public HrCandidateController(IHrCandidateService hrCandidateService)
        {
            _hrCandidateService = hrCandidateService;
        }
        [HttpPost("GetCandidatelList")]
        public IActionResult GetCandidatelList(QueryCandidate query)
        {
            return Ok(_hrCandidateService.GetCandidatelList(query));
        }
        [HttpPost("GetCandidateDetail")]
        public IActionResult GetCandidateDetail(QueryCandidate query)
        {
            var detail = _hrCandidateService.GetCandidateDetail(query);
            return Ok(detail);
        }
        [HttpPost("Update")]
        public IActionResult UpdateCandidate(ViewhrCandidate data)
        {
            try
            {
                _hrCandidateService.UpdateCandidate(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequestResult(ex.Message);
            }
        }
        [HttpPost("DeleteList")]
        public IActionResult DeleteList(ViewhrCandidate data)
        {
            try
            {
                return Ok(_hrCandidateService.DeleteList(data));
            }
            catch (Exception ex)
            {
                return this.BadRequestResult(ex.Message);
            }
        }
        [HttpPost("AddList")]
        public IActionResult AddList(ViewhrCandidate data)
        {
            try
            {
                return Ok(_hrCandidateService.AddList(data));
            }
            catch (Exception ex)
            {
                return this.BadRequestResult(ex.Message);
            }
        }
    }
}
