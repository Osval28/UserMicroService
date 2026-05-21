using UserService.Application.Exceptions;
using UserService.Infrastructure.Repositories;

namespace UserService.Application.UseCase
{
    public class AssignBusinessUseCase
    {
        private readonly UserRepository _userRepository;
        private readonly InvitationRepository _invitationRepository;

        public AssignBusinessUseCase(UserRepository userRepository, InvitationRepository invitationRepository)
        {
            _userRepository = userRepository;
            _invitationRepository = invitationRepository;
        }

        public void Execute(string invitationCodeValue, int userId)
        {
            if (_invitationRepository.IsValid(invitationCodeValue))
            {
                var invitationCode = _invitationRepository.GetByCode(invitationCodeValue);

                _userRepository.AssignBusiness(userId, invitationCode.BusinessId, invitationCode.RoleId);

                _invitationRepository.MarkAsUsed(invitationCodeValue);
            }
            else
            {
                throw new BusinessException("Código de invitación no válido");
            }
        }
    }
}
