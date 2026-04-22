using UserService.Application.UseCase;

namespace UserService.Application.Services
{
    public class InvitationService
    {
        private readonly GenerateInvitationCodeUseCase _generateCodeUseCase;
        private readonly JoinWithCodeUseCase _joinWithCodeUseCase;

        public InvitationService(GenerateInvitationCodeUseCase generateCodeUseCase, JoinWithCodeUseCase joinWithCodeUseCase)
        {
            _generateCodeUseCase = generateCodeUseCase;
            _joinWithCodeUseCase = joinWithCodeUseCase;
        }

        public string GenerateCode(int businessId, int roleId)
        {
            var invitation = _generateCodeUseCase.Execute(businessId, roleId);
            return invitation.Code;
        }

        public void JoinWithCode(string code, int userId)
        {
            _joinWithCodeUseCase.Execute(code, userId);
        }
    }
}