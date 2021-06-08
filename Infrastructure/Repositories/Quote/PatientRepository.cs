using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Base;

namespace Infrastructure.Repositories
{
    class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(IDbContext context) : base(context)
        {

        }
    }
}
