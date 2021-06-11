using Application.Base;
using Application.Http.Requests;
using Application.Http.Responses.QuoteResponse;
using Application.Services;
using Domain.Contract;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;

        public PatientController( IUnitOfWork unitOfWork)
        {
            _patientService = new PatientService(unitOfWork);
        }

        [HttpPost("CreatePatient")]
        public ActionResult<Response<PatientResponse>> CreatePatient([FromBody] PatientRequest patientRequest)
        {
            var response = _patientService.Create(patientRequest);
            return StatusCode((int)response.Code, response);
        }

        [HttpPut("UpdatePatient{identification}")]
        public ActionResult<Response<PatientResponse>> UpdatePatient(string identification, [FromBody] Patient patient)
        {
            var response = _patientService.Update(identification, patient);
            return StatusCode((int)response.Code, response);
        }

        [HttpDelete("DeletePatient{identification}")]
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

        [HttpGet("GetPatientActivateds")]
        public ActionResult<Response<PatientResponse>> GetActivateds()
        {
            var response = _patientService.GetActivateds();
            return StatusCode((int)response.Code, response);
        }
    }
}
