using Application.Base;
using Application.Http.Requests;
using Application.Http.Responses.QuoteResponse;
using Application.Services;
using Domain.Contract;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareStaffController : ControllerBase
    {
        private readonly CareStaffService _careStaffService;

        public CareStaffController(IUnitOfWork unitOfWork)
        {
            _careStaffService = new CareStaffService(unitOfWork);
        }

        [HttpPost("CreateCareStaff")]
        public ActionResult<Response<CareStaffResponse>> CreateCareStaff([FromBody] CareStaffRequest careStaffRequest)
        {
            var response = _careStaffService.Create(careStaffRequest);
            return StatusCode((int)response.Code, response);
        }

        [HttpPut("UpdateCareStaff/{identification}")]
        public ActionResult<Response<CareStaffResponse>> UpdateCareStaff(string identification, [FromBody] CareStaffRequestUpdate careStaffRequestUpdated)
        {
            var response = _careStaffService.Update(identification, careStaffRequestUpdated);
            return StatusCode((int)response.Code, response);
        }

        [HttpDelete("DeleteCareStaff/{identification}")]
        public ActionResult<Response<CareStaffResponse>> DeleteCareStaff(string identification)
        {
            var response = _careStaffService.Delete(identification);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("GetAllCareStaff")]
        public ActionResult<Response<CareStaffResponse>> GetAllCareStaff()
        {
            var response = _careStaffService.GetAll();
            return StatusCode((int)response.Code,response);
        }

        [HttpGet("GetFilterForType/{type}")]
        public ActionResult<Response<CareStaffResponse>> GetFilterForType(string type)
        {
            var response = _careStaffService.GetFilterForType(type);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("SearchCareStaff/{identification}")]
        public ActionResult<Response<CareStaffResponse>> SearchCareStaff(string identification)
        {
            var response = _careStaffService.Search(identification);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("GetPatientsWithQuote/{identification}")]
        public ActionResult<Response<List<QuoteAssignedToCareStaff>>> GetPatientsWithQuote(string identification)
        {
            var response = _careStaffService.GetPatientsWithQuote(identification);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("GetCareStaffActivateds")]
        public ActionResult<Response<CareStaffResponse>> GetActivateds()
        {
            var response = _careStaffService.GetActivateds();
            return StatusCode((int)response.Code, response);
        }
    }
}
