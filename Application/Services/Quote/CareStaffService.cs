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

namespace Application.Services.Quote
{
    public class CareStaffService : Service<CareStaff>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ICareStaffRepository _careStaffRepository;

        public CareStaffService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _careStaffRepository = unitOfWork.CareStaffRepository;
            _patientRepository = unitOfWork.PatientRepository;
        }

        public Response<CareStaffResponse> Create(CareStaffRequest CareStaffRequest)
        {
            try
            {
                if (CareStaffIsRegistered(CareStaffRequest.Identification))
                {
                    return Response<CareStaffResponse>.CreateResponseFailed($"El personal de atencion (a) con identificacion : {CareStaffRequest.Identification} ya se encuentra registrado ", HttpStatusCode.BadRequest);
                }
                CareStaff CareStaff = MapCareStaff(CareStaffRequest);
                _careStaffRepository.Add(CareStaff);
                UnitOfWork.Commit();
                return Response<CareStaffResponse>.CreateResponseSuccess($"personal de atencion {CareStaffRequest.Name} registrado con exito", HttpStatusCode.Created);
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
                List<CareStaff> CareStaffs = (List<CareStaff>)_careStaffRepository.FindBy(careStaff => careStaff.State == true).ToList();
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

        private List<CareStaffResponse> ConvertListCareStaffsResponse(List<CareStaff> CareStaffs)
        {
            return CareStaffs.ConvertAll(CareStaff => new CareStaffResponse(CareStaff));
        }

        private CareStaff MapCareStaff(CareStaffRequest careStaffRequest)
        {
            return new CareStaff
            {
                Identification = careStaffRequest.Identification,
                Name = careStaffRequest.Name,
                Surname = careStaffRequest.Surname,
                Photo = careStaffRequest.Photo,
                State = true,
                Type = careStaffRequest.Type
            };
        }

        private CareStaff MapCareStaff(CareStaffRequestUpdate careStaffRequestUpdated)
        {
            return new CareStaff
            {
                Id = careStaffRequestUpdated.Id,
                Identification = careStaffRequestUpdated.Identification,
                Name = careStaffRequestUpdated.Name,
                Surname = careStaffRequestUpdated.Surname,
                Photo = careStaffRequestUpdated.Photo,
                Type = careStaffRequestUpdated.Type
            };
        }

    }
}
