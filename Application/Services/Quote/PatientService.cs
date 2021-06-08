using Application.Base;
using Domain.Contract;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class PatientService : Service<Patient>
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _patientRepository = unitOfWork.PatientRepository;
        }

        public Response<Patient> CreatePatient() 
        {
            
        }
    }
}
