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
    public class PatientService : Service<Patient>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ICareStaffRepository _careStaffRepository;

        public PatientService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _patientRepository = unitOfWork.PatientRepository;
            _careStaffRepository = unitOfWork.CareStaffRepository;
        }

        public Response<PatientResponse> Create(PatientRequest patientRequest)
        {
            try
            {
                if (PatientIsRegistered(patientRequest.Identification))
                {
                    return Response<PatientResponse>.CreateResponseFailed($"El paciente (a) con identificacion : {patientRequest.Identification} ya se encuentra registrado ", HttpStatusCode.BadRequest);
                }
                Patient patient = patientRequest.MapPatient();
                _patientRepository.Add(patient);
                UnitOfWork.Commit();
                return Response<PatientResponse>.CreateResponseSuccess($"Paciente {patientRequest.Name} registrado con exito", HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                return Response<PatientResponse>.CreateResponseFailed(e.Message, HttpStatusCode.InternalServerError);
            }
        }

        public Response<List<PatientResponse>> GetActivateds()
        {
            try
            {
                List<Patient> patients = _patientRepository.FindBy(p => p.State == true, "Direction").ToList();
                List<PatientResponse> patientResponses = ConvertListPatientsResponse(patients);
                return Response<List<PatientResponse>>.CreateResponseSuccess($"Pacientes consultado con exito, Resultados : {patientResponses.Count}", HttpStatusCode.OK, patientResponses);
            }
            catch (Exception e)
            {
                return Response<List<PatientResponse>>.CreateResponseFailed(e.Message, HttpStatusCode.BadRequest);
            }
        }

        public Response<List<PatientResponse>> GetAll()
        {
            try
            {
                List<Patient> patients = _patientRepository.FindBy(p => p != null, "Direction", patient => patient.OrderBy(p => p.Id)).ToList();
                List<PatientResponse> patientResponses = ConvertListPatientsResponse(patients);
                return Response<List<PatientResponse>>.CreateResponseSuccess($"Pacientes consultado con exito, Resultados : {patientResponses.Count}", HttpStatusCode.OK, patientResponses);
            }
            catch (Exception e)
            {
                return Response<List<PatientResponse>>.CreateResponseFailed(e.Message, HttpStatusCode.BadRequest);
            }
        }

        public Response<PatientResponse> Update(string identification,PatientRequestUpdate patientUpdated) 
        {
            try
            {
                if (PatientIsRegistered(identification))
                {
                    _patientRepository.Edit(MapPatient(patientUpdated));
                    UnitOfWork.Commit();
                    return Response<PatientResponse>.CreateResponseSuccess($"El paciente {patientUpdated.Name} fue actualizado con exito", HttpStatusCode.OK);
                }
                return Response<PatientResponse>.CreateResponseFailed($"El paciente (a) con identificacion no se encuentra registrado", HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                return Response<PatientResponse>.CreateResponseFailed(e.Message, HttpStatusCode.InternalServerError);
            }
        }

        public Response<PatientResponse> Delete(string identification)
        {
            try
            {
                if (PatientIsRegistered(identification))
                {
                    Patient patient = _patientRepository.FindFirstOrDefault(patient => patient.Identification == identification);
                    _patientRepository.Delete(patient);
                    UnitOfWork.Commit();
                    return Response<PatientResponse>.CreateResponseSuccess($"El paciente (a) con identificacion : {identification} fue elimido con exito", HttpStatusCode.OK);
                }
                return Response<PatientResponse>.CreateResponseFailed($"El paciente (a) con identificacion no se encuentra registrado", HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                return Response<PatientResponse>.CreateResponseFailed(e.Message, HttpStatusCode.InternalServerError);
            }
        }

        private bool PatientIsRegistered(string identification)
        {
            Patient patient = _patientRepository.FindFirstOrDefault(patient => patient.Identification == identification);
            CareStaff careStaff = _careStaffRepository.FindFirstOrDefault(careStaff => careStaff.Identification == identification);
            return patient != null || careStaff != null ? true : false;
        }

        private List<PatientResponse> ConvertListPatientsResponse(List<Patient> patients)
        {
            return patients.ConvertAll(patient => new PatientResponse(patient).Include(patient.Direction));
        }

        private Patient MapPatient(PatientRequestUpdate patientRequestUpdated)
        {
            Patient patient = _patientRepository.FindFirstOrDefault(p => p.Identification == patientRequestUpdated.Identification);
            patient = patientRequestUpdated.MapPatient(patient);
            return patient;
        }

    }

}
