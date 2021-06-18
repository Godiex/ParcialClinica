
using Application.Base;
using Application.Http.Requests;
using Application.Http.Responses.QuoteResponse;
using Domain.Contract;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Application.Services
{
    public class QuoteService : Service<Quote>
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly ICareStaffRepository _careStaffRepository;
        private readonly IPatientRepository _patientRepository;
        public QuoteService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _quoteRepository = unitOfWork.QuoteRepository;
            _patientRepository = unitOfWork.PatientRepository;
            _careStaffRepository = unitOfWork.CareStaffRepository;
        }

        public Response<QuoteResponse> Create(QuoteRequest quoteRequest)
        {
            try
            {
                Quote quote = MapQuote(quoteRequest);
                _quoteRepository.Add(quote);
                UnitOfWork.Commit();
                return Response<QuoteResponse>.CreateResponseSuccess("Cita registrada con exito",HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                return Response<QuoteResponse>.CreateResponseFailed(e.Message, HttpStatusCode.InternalServerError);
            }
        }

        public Response<List<QuoteResponse>> GetAllQuotes()
        {
            try
            {
                List<Quote> quotes = _quoteRepository.FindBy(p => p != null, "Patient.Direction,CareStaff", q => q.OrderBy(p => p.Id)).ToList();
                List<QuoteResponse> quotesResponse = ConvertListQuotesResponse(quotes);
                return Response<List<QuoteResponse>>.CreateResponseSuccess($"Citas consultadas con exito, Resultados : {quotesResponse.Count}", HttpStatusCode.OK, quotesResponse);
            }
            catch (Exception e)
            {
                return Response<List<QuoteResponse>>.CreateResponseFailed(e.Message, HttpStatusCode.BadRequest);
            }
        }

        private List<QuoteResponse> ConvertListQuotesResponse(List<Quote> quotes)
        {
            return quotes.ConvertAll(quote => new QuoteResponse(quote).Include(quote.CareStaff, quote.Patient)).ToList();
        }

        public Quote MapQuote(QuoteRequest quoteRequest)
        {
            Quote quote = quoteRequest.MapQuote();
            quote.Patient = _patientRepository.FindFirstOrDefault(p => p.Identification == quoteRequest.IdentificationPatient);
            List<CareStaff> careStaffs = FillCareStaff(quoteRequest);
            quote.AddRangeCareStaff(careStaffs);
            return quote;
        }

        private List<CareStaff> FillCareStaff(QuoteRequest quote)
        {
            List<CareStaff> careStaffs = new List<CareStaff>();
            foreach (var item in quote.CareStaff)
            {
                CareStaff careStaff = _careStaffRepository.FindFirstOrDefault(c => c.Identification == item.IdentificationCareStaff);
                careStaffs.Add(careStaff);
            }
            return careStaffs;
        }
    }
}
