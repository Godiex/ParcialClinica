using System;
using Domain.Repositories;

namespace Domain.Contract
{
    public interface IUnitOfWork : IDisposable
    {
        #region [Repositories]
        public IUserRepository UserRepository { get;}
        public ICareStaffRepository CareStaffRepository { get; }
        public IPatientRepository PatientRepository { get; }
        public IQuoteRepository QuoteRepository { get; }
        public IRoleRepository RoleRepository { get; }
        #endregion

        int Commit();
    }
}