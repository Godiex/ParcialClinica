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
    public class QuoteController : ControllerBase
    {
        private readonly QuoteService _quoteService;

        public QuoteController(IUnitOfWork unitOfWork)
        {
            _quoteService = new QuoteService(unitOfWork);
        }

        [HttpPost("CreateQuote")]
        public ActionResult<Response<QuoteResponse>> CreateQuote([FromBody] QuoteRequest quoteRequest)
        {
            var response = _quoteService.Create(quoteRequest);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("GetAllQuote")]
        public ActionResult<Response<QuoteResponse>> GetAllQuote()
        {
            var response = _quoteService.GetAllQuotes();
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("SearchQuote")]
        public ActionResult<Response<CareStaffResponse>> SearchQuote(int id)
        {
            var response = _quoteService.Search(id);
            return StatusCode((int)response.Code, response);
        }

        [HttpPut("UpdateQuote")]
        public ActionResult<Response<QuoteResponse>> UpdateQuote(QuoteRequestUpdated quoteRequestUpdated)
        {
            var response = _quoteService.Update(quoteRequestUpdated);
            return StatusCode((int)response.Code, response);
        }

        [HttpPut("AnulatedQuote")]
        public ActionResult<Response<QuoteResponse>> UpdateQuote(int idQuote)
        {
            var response = _quoteService.AnulatedQuote(idQuote);
            return StatusCode((int)response.Code, response);
        }
    }
}
