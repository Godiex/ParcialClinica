using System;
using System.Net;
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
            try
            {
                User user = _userRepository.FindFirstOrDefault(x => x.Username == request.Username && x.Password == Hash.GetSha256(request.Password));
                LoginUserResponse userResponse = new LoginUserResponse(user.Username);
                return Response<LoginUserResponse>.CreateResponseSuccess("Usuario encontrado con exito",HttpStatusCode.OK, userResponse);
            }
            catch (Exception e)
            {
                return Response<LoginUserResponse>.CreateResponseFailed($"Error al iniciar sesion {e.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public Response<CreateUserResponse> CreateUser(UserRequest userRequest)
        {
            try
            {
                User user = MapUser(userRequest);
                _userRepository.Add(user);
                UnitOfWork.Commit();
                CreateUserResponse userResponse = new CreateUserResponse(user.Username);
                return Response<CreateUserResponse>.CreateResponseSuccess($"Usuario {user.Username} creado con exito",HttpStatusCode.Created,userResponse);
            }
            catch (Exception e)
            {
                return Response<CreateUserResponse>.CreateResponseFailed($"Error al crear el usuario {e.Message}", HttpStatusCode.InternalServerError);
            } 
        }

        private User MapUser(UserRequest userRequest) 
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