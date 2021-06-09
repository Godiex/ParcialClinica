using Application.Base;
using Application.Http.Requests;
using Application.Http.Responses.QuoteResponse;
using Application.Services;
using Domain.Contract;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers.Quote
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly PatientService _patientService;

        public PatientController(ILogger<UserController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _patientService = new PatientService(unitOfWork);
        }

        [HttpPost("CreatePatient")]
        public ActionResult<Response<PatientResponse>> CreatePatient(PatientRequest patientRequest)
        {
            var response = _patientService.Create(patientRequest);
            return StatusCode((int)response.Code, response);
        }

        [HttpPut("UpdatePatient")]
        public ActionResult<Response<PatientResponse>> UpdatePatient(Patient patient)
        {
            var response = _patientService.Update(patient);
            return StatusCode((int)response.Code, response);
        }

        [HttpDelete("DeletePatient")]
        public ActionResult<Response<PatientResponse>> DeletePatient(string identification)
        {
            var response = _patientService.Delete(identification);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("GetAllPatient")]
        public ActionResult<Response<PatientResponse>> GetAllPatient()
        {
            var response = _patientService.GetAll();
            return StatusCode((int)response.Code, response);
        }
    }
}
