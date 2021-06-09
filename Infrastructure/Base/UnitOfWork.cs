using Domain.Contract;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbContext _dbContext;

        public UnitOfWork(IDbContext context)
        {
            _dbContext = context;
        }

        #region Repositories

        private IUserRepository _userRepository;
        private ICareStaffRepository _careStaffRepository;
        private IPatientRepository _patientRepository;
        private IQuoteRepository _quoteRepository;

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_dbContext);
        public ICareStaffRepository CareStaffRepository => _careStaffRepository ??= new CareStaffRepository(_dbContext);
        public IPatientRepository PatientRepository => _patientRepository ??= new PatientRepository(_dbContext);
        public IQuoteRepository QuoteRepository => _quoteRepository ??= new QuoteRepository(_dbContext);
        #endregion

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing || _dbContext == null) return;
            ((DbContext) _dbContext).Dispose();
            _dbContext = null;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
    }
}