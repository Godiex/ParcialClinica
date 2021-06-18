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
    }
}
