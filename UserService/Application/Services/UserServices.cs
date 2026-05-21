using UserService.Application.DTO_s;
using UserService.Application.DTOs;
using UserService.Application.UseCase;
using UserService.Infrastructure;

namespace UserService.Application.Services
{
    public class UserServices
    {
        private readonly CreateUserUseCase _createUserUseCase;

        public UserServices(CreateUserUseCase createUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
        }

        public UserEntity CreateUser(UserDTO dto)
        {
            var user = dto.FromDTOtoModel();
            return _createUserUseCase.Execute(user);
        }

    }
}