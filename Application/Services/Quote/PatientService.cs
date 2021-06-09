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
        
        public PatientService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _patientRepository = unitOfWork.PatientRepository;
        }

        public Response<PatientResponse> Create(PatientRequest patientRequest)
        {
            if (PatientIsRegistered(patientRequest.Identification))
            {
                return Response<PatientResponse>.CreateResponseFailed($"El paciente (a) con identificacion : {patientRequest.Identification} ya se encuentra registrado ", HttpStatusCode.NoContent);
            }
            Patient patient = MapPatient(patientRequest);
            _patientRepository.Add(patient);
            UnitOfWork.Commit();
            return Response<PatientResponse>.CreateResponseSuccess($"Paciente {patientRequest.Name} registrado con exito", HttpStatusCode.Created);
        }

        public Response<List<PatientResponse>> GetActivateds()
        {
            List<Patient> patients = (List<Patient>)_patientRepository.FindBy(patient => patient.State == true);
            List<PatientResponse> patientResponses = ConvertListPatientsResponse(patients);
            return Response<List<PatientResponse>>.CreateResponseSuccess($"Pacientes consultado con exito, Resultados : {patientResponses.Count}", HttpStatusCode.OK, patientResponses);
        }

        public Response<List<PatientResponse>> GetAll()
        {
            List<Patient> patients = (List<Patient>)_patientRepository.GetAll(patient => patient.OrderBy(p => p.Id));
            List<PatientResponse> patientResponses = ConvertListPatientsResponse(patients);
            return Response<List<PatientResponse>>.CreateResponseSuccess($"Pacientes consultado con exito, Resultados : {patientResponses.Count}", HttpStatusCode.OK, patientResponses);
        }

        public Response<PatientResponse> Update(Patient patientUpdated) 
        {
            if (PatientIsRegistered(patientUpdated.Identification))
            {
                _patientRepository.Edit(patientUpdated);
                UnitOfWork.Commit();
                return Response<PatientResponse>.CreateResponseSuccess($"El paciente {patientUpdated.Name} fue actualizado con exito", HttpStatusCode.OK);
            }
            return Response<PatientResponse>.CreateResponseFailed($"El paciente (a) con identificacion no se encuentra registrado", HttpStatusCode.NoContent);
        }

        public Response<PatientResponse> Delete(string identification)
        {
            if (PatientIsRegistered(identification))
            {
                Patient patient = _patientRepository.FindFirstOrDefault(patient => patient.Identification == identification);
                _patientRepository.Delete(patient);
                UnitOfWork.Commit();
                return Response<PatientResponse>.CreateResponseSuccess($"El paciente (a) con identificacion : {identification} fue elimido con exito", HttpStatusCode.OK);
            }
            return Response<PatientResponse>.CreateResponseFailed($"El paciente (a) con identificacion no se encuentra registrado", HttpStatusCode.NoContent);
        }

        private bool PatientIsRegistered(string identification)
        {
            Patient patient = _patientRepository.FindFirstOrDefault(patient => patient.Identification == identification);
            return patient != null ? true : false;
        }

        private List<PatientResponse> ConvertListPatientsResponse(List<Patient> patients)
        {
            return patients.ConvertAll(patient => new PatientResponse(patient).Include(patient.Direction));
        }

        private Patient MapPatient(PatientRequest patientRequest)
        {
            Patient patient = new Patient
            {
                Identification = patientRequest.Identification,
                Name = patientRequest.Name,
                Surname = patientRequest.Surname,
                DateOfBirth = patientRequest.DateOfBirth,
                Photo = patientRequest.Photo,
                Direction = MapDirection(patientRequest.Direction),
                State = true
            };
            return patient;
        }

        private Direction MapDirection(DirectionRequest directionRequest) 
        {
            Direction direction = new Direction(directionRequest.Nomenclature,directionRequest.City, directionRequest.Neighborhood);
            return direction;
        }

    }

}
