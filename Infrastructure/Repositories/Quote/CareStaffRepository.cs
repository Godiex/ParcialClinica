
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Base;

namespace Infrastructure.Repositories
{
    public class CareStaffRepository : GenericRepository<CareStaff>, ICareStaffRepository
    {
        public CareStaffRepository(IDbContext context) : base(context)
        {

        }
    }
}
