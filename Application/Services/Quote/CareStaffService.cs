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
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CareStaffService : Service<CareStaff>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ICareStaffRepository _careStaffRepository;
        private readonly UserService userService;
        public CareStaffService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _careStaffRepository = unitOfWork.CareStaffRepository;
            _patientRepository = unitOfWork.PatientRepository;
            userService = new UserService(unitOfWork);
        }

        public Response<CareStaffResponse> Create(CareStaffRequest CareStaffRequest)
        {
            try
            {
                if (CareStaffIsRegistered(CareStaffRequest.Identification))
                {
                    return Response<CareStaffResponse>.CreateResponseFailed($"El personal de atencion (a) con identificacion : {CareStaffRequest.Identification} ya se encuentra registrado ", HttpStatusCode.BadRequest);
                }
                CareStaff careStaff = CareStaffRequest.MapCareStaff();
                careStaff.User = userService.MapUser(CareStaffRequest.User);
                _careStaffRepository.Add(careStaff);
                UnitOfWork.Commit();
                return Response<CareStaffResponse>.CreateResponseSuccess($"personal de atencion {careStaff.Name} registrado con exito", HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                return Response<CareStaffResponse>.CreateResponseFailed(e.Message, HttpStatusCode.BadRequest);
            }
        }

        public Response<List<CareStaffResponse>> GetActivateds()
        {
            try
            {
                List<CareStaff> CareStaffs = _careStaffRepository.FindBy(c => c != null, "Quotes", careStaff => careStaff.OrderBy(p => p.Id)).ToList();
                List<CareStaffResponse> CareStaffResponses = ConvertListCareStaffsResponse(CareStaffs);
                return Response<List<CareStaffResponse>>.CreateResponseSuccess($"personal de atencions consultado con exito, Resultados : {CareStaffResponses.Count}", HttpStatusCode.OK, CareStaffResponses);
            }
            catch (Exception e)
            {
                return Response<List<CareStaffResponse>>.CreateResponseFailed(e.Message, HttpStatusCode.BadRequest);
            }
            
        }

        public Response<List<CareStaffResponse>> GetAll()
        {
            try
            {
                List<CareStaff> CareStaffs = (List<CareStaff>)_careStaffRepository.GetAll(careStaff => careStaff.OrderBy(p => p.Id)).ToList();
                List<CareStaffResponse> CareStaffResponses = ConvertListCareStaffsResponse(CareStaffs);
                return Response<List<CareStaffResponse>>.CreateResponseSuccess($"personal de atencions consultado con exito, Resultados : {CareStaffResponses.Count}", HttpStatusCode.OK, CareStaffResponses);
            }
            catch (Exception e)
            {
                return Response<List<CareStaffResponse>>.CreateResponseFailed(e.Message, HttpStatusCode.BadRequest);
            }
        }

        public Response<List<QuoteAssignedToCareStaff>> GetPatientsWithQuote(string identification)
        {
            try
            {
                CareStaff careStaffs = _careStaffRepository.FindBy(c => c.Identification == identification, "Quotes.Patient.Direction", careStaff => careStaff.OrderBy(p => p.Id)).ToList().First();
                List<QuoteAssignedToCareStaff> quotesAssignedsToCareStaff = FillQuoteAssignedToCareStaff(careStaffs);

                return Response<List<QuoteAssignedToCareStaff>>.CreateResponseSuccess($"Citas del personal de atencion consultado con exito", HttpStatusCode.OK, quotesAssignedsToCareStaff);
            }
            catch (Exception e)
            {
                return Response<List<QuoteAssignedToCareStaff>>.CreateResponseFailed(e.Message, HttpStatusCode.BadRequest);
            }
        }

        private List<QuoteAssignedToCareStaff> FillQuoteAssignedToCareStaff(CareStaff careStaffs)
        {
            List<QuoteAssignedToCareStaff> quotesAssignedsToCareStaff = new List<QuoteAssignedToCareStaff>();
            foreach (var item in careStaffs.Quotes)
            {
                if (!item.State.Equals("Anulada") && !item.State.Equals("Atendido"))
                {
                    QuoteAssignedToCareStaff quoteAssignedToCareStaff = new QuoteAssignedToCareStaff(item).Include(item.Patient);
                    quotesAssignedsToCareStaff.Add(quoteAssignedToCareStaff);
                }
            }

            return quotesAssignedsToCareStaff;
        }

        public Response<CareStaffResponse> Update(string identification, CareStaffRequestUpdate careStaffRequestUpdated)
        {
            try
            {
                if (CareStaffIsRegistered(identification))
                {
                    _careStaffRepository.Edit(MapCareStaff(careStaffRequestUpdated));
                    UnitOfWork.Commit();
                    return Response<CareStaffResponse>.CreateResponseSuccess($"El personal de atencion {careStaffRequestUpdated.Name} fue actualizado con exito", HttpStatusCode.OK);
                }
                return Response<CareStaffResponse>.CreateResponseFailed($"El personal de atencion (a) con identificacion no se encuentra registrado", HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                return Response<CareStaffResponse>.CreateResponseFailed(e.Message, HttpStatusCode.InternalServerError);
            } 
        }

        public Response<CareStaffResponse> Delete(string identification)
        {
            try
            {
                if (CareStaffIsRegistered(identification))
                {
                    CareStaff CareStaff = _careStaffRepository.FindFirstOrDefault(CareStaff => CareStaff.Identification == identification);
                    CareStaff.State = false;
                    _careStaffRepository.Edit(CareStaff);
                    UnitOfWork.Commit();
                    return Response<CareStaffResponse>.CreateResponseSuccess($"El personal de atencion (a) con identificacion : {identification} fue elimido con exito", HttpStatusCode.OK);
                }
                return Response<CareStaffResponse>.CreateResponseFailed($"El personal de atencion (a) con identificacion no se encuentra registrado", HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                return Response<CareStaffResponse>.CreateResponseFailed(e.Message, HttpStatusCode.BadRequest);
            }
        }

        private bool CareStaffIsRegistered(string identification)
        {
            Patient patient = _patientRepository.FindFirstOrDefault(patient => patient.Identification == identification && patient.State == true);
            CareStaff careStaff = _careStaffRepository.FindFirstOrDefault(careStaff => careStaff.Identification == identification && careStaff.State == true);
            return careStaff != null || patient != null ? true : false;
        }

        private List<CareStaffResponse> ConvertListCareStaffsResponse(List<CareStaff> careStaffs)
        {
            DateTime date = DateTime.Now;
            
            foreach (var item in careStaffs)
            {
                SetWorkStatus(date, item);
            }
            List<CareStaffResponse> careStaffResponses = careStaffs.ConvertAll(CareStaff => new CareStaffResponse(CareStaff));
            return careStaffResponses;
        }

        private void SetWorkStatus(DateTime date, CareStaff item)
        {
            foreach (var quote in item.Quotes)
            {
                if (ItsDayQuote(date, quote))
                {
                    if (date.Hour >= quote.StartTime.Hour && date.Hour <= quote.EndTime.Hour -1)
                    {
                        item.WorkStatus = true;
                    }
                }
            }
        }

        private bool ItsDayQuote(DateTime date, Quote quote)
        {
            return date.Day == quote.Date.Day && date.Month == quote.Date.Month && date.Year == quote.Date.Year;
        }

        private CareStaff MapCareStaff(CareStaffRequestUpdate careStaffRequestUpdated)
        {
            CareStaff careStaff = _careStaffRepository.FindFirstOrDefault(c => c.Identification == careStaffRequestUpdated.Identification);
            careStaff = careStaffRequestUpdated.MapCareStaff(careStaff);
            return careStaff;
        }

    }
}
