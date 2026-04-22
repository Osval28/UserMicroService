using UserService.Application.Exceptions;
using UserService.Infrastructure;
using UserService.Infrastructure.Repositories;

namespace UserService.Application.UseCase
{
    public class LoginUseCase
    {
        private readonly UserRepository _userRepository;

        public LoginUseCase(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserEntity Execute(string identificador, string password)
        {
            var user = _userRepository.GetByUsernameOrEmailWithRole(identificador);

            if (user == null)
                throw new BusinessException("Invalid credentials");

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new BusinessException("Invalid credentials");

            return user;
        }
    }
}