using UserService.Application.Exceptions;
using UserService.Domain.Model;
using UserService.Infrastructure;
using UserService.Infrastructure.Repositories;

namespace UserService.Application.UseCase
{
    public class CreateUserUseCase
    {
        private readonly UserRepository _userRepository;

        public CreateUserUseCase(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserEntity Execute(User user)
        {
            if (_userRepository.ExistsByEmail(user.Email))
                throw new BusinessException("Email already registered");

            if (_userRepository.ExistsByUsername(user.Username))
                throw new BusinessException("Username already registered");

            if (_userRepository.ExistsByPhone(user.Phone))
                throw new BusinessException("Phone already registered");

            user.RoleId = user.RegisterType == RegisterType.Personal ? 3 : 1;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var newUserDB = new UserEntity
            {
                Name = user.Name,
                LastName = user.LastName,
                Phone = user.Phone,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                RegisterType = user.RegisterType,
                RoleId = user.RoleId,
                BusinessId = user.BusinessId
            };

            return _userRepository.CreateUser(newUserDB);
        }
    }
}