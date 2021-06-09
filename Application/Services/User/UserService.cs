using System;
using Application.Base;
using Application.Http.Requests;
using Application.Http.Responses;
using Domain.Contract;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Encrypt;

namespace Application.Services
{
    public class UserService : Service<User>
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _userRepository = unitOfWork.UserRepository;
        }

        public Response<LoginUserResponse> LoginUser(UserLoginRequest request)
        {
            User user = _userRepository.FindFirstOrDefault(x => x.Username == request.Username && x.Password == Hash.GetSha256(request.Password));
            throw new NotImplementedException();
        }

        public Response<CreateUserResponse> CreateUser(UserRequest userRequest)
        {
            User user = MapUser(userRequest);
            _userRepository.Add(user);
            throw new NotImplementedException();
        }

        public User MapUser(UserRequest userRequest) 
        {
            User user = new User
            {
                Username = userRequest.Username,
                Password = Hash.GetSha256(userRequest.Password)
            };
            return user;
        }
    }
}