using UserService.Application.DTOs;
using UserService.Application.UseCase;
using UserService.Infrastructure.Services;

namespace UserService.Application.Services
{
    public class AuthService
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly JWTService _jwtService;

        public AuthService(LoginUseCase loginUseCase, JWTService jwtService)
        {
            _loginUseCase = loginUseCase;
            _jwtService = jwtService;
        }

        public string Login(LoginDTO dto)
        {
            var user = _loginUseCase.Execute(dto.Identificador, dto.Password);
            return _jwtService.GenerateToken(user.Username, user.Role.Name);
        }
    }
}