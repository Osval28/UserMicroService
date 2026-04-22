using UserService.Application.Exceptions;
using UserService.Infrastructure.Repositories;

namespace UserService.Application.UseCase
{
    public class JoinWithCodeUseCase
    {
        private readonly InvitationCodeRepository _invitationCodeRepository;
        private readonly UserRepository _userRepository;

        public JoinWithCodeUseCase(InvitationCodeRepository invitationCodeRepository, UserRepository userRepository)
        {
            _invitationCodeRepository = invitationCodeRepository;
            _userRepository = userRepository;
        }

        public void Execute(string code, int userId)
        {
            var invitationCode = _invitationCodeRepository.GetByCode(code);

            if (invitationCode == null)
                throw new BusinessException("Invalid invitation code");

            if (invitationCode.IsUsed)
                throw new BusinessException("Invitation code has already been used");

            if (invitationCode.ExpiresAt < DateTime.Now)
                throw new BusinessException("Invitation code has expired");

            _userRepository.AssignBusiness(userId, invitationCode.BusinessId, invitationCode.RoleId);
            _invitationCodeRepository.MarkAsUsed(invitationCode);
        }
    }
}