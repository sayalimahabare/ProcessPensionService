using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using ProcessPensionDetails.Models;
using ProcessPensionDetails.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionDetails.Controllers
{
    [Route("api/processPension")]
    [ApiController]
    [Authorize]
    public class ProcessPensionerDetailsController : ControllerBase
    {
        private readonly IProcessDetailsRepository _process;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProcessPensionerDetailsController));

        public ProcessPensionerDetailsController(IProcessDetailsRepository process)
        {
            _process = process;
        }

        [HttpPost("[action]/{aadharNumber}")]
        public async Task<IActionResult> GetProcessdetailsAsync(string aadharNumber)
        {
            _log4net.Info(" Http POST Request From GetProcessdetailsAsync method of: " + nameof(ProcessPensionerDetailsController));
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");


            var detaillist = await _process.GetListAsync(aadharNumber,token);
            if (detaillist.Any())
            {
                var pensionerDetail = detaillist[0];

                var result = _process.CalculatePension(pensionerDetail);

                await _process.SaveDataAsync(token);
                await _process.Update(result);

                _log4net.Info("processed details of pensioners return From GetProcessdetailsAsync method of: " + nameof(ProcessPensionerDetailsController));
                return Ok(result);

                
            }
            _log4net.Error(" bad Request returned From GetProcessdetailsAsync method of: " + nameof(ProcessPensionerDetailsController));
            return BadRequest();
        }
    }
}
