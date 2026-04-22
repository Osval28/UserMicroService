using UserService.Application.Exceptions;
using UserService.Infrastructure;
using UserService.Infrastructure.Repositories;

namespace UserService.Application.UseCase
{
    public class GenerateInvitationCodeUseCase
    {
        private readonly InvitationCodeRepository _invitationCodeRepository;

        public GenerateInvitationCodeUseCase(InvitationCodeRepository invitationCodeRepository)
        {
            _invitationCodeRepository = invitationCodeRepository;
        }

        public InvitationCode Execute(int businessId, int roleId)
        {
            var code = "DAZMA-" + GenerateRandomCode();

            var invitationCode = new InvitationCode
            {
                Code = code,
                BusinessId = businessId,
                RoleId = roleId,
                ExpiresAt = DateTime.Now.AddHours(24),
                IsUsed = false
            };

            return _invitationCodeRepository.CreateCode(invitationCode);
        }

        private string GenerateRandomCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Range(0, 5)
                .Select(_ => chars[random.Next(chars.Length)])
                .ToArray());
        }
    }
}