using Microsoft.EntityFrameworkCore;
using UserService.Application.Exceptions;
using UserService.Infrastructure;

namespace UserService.Infrastructure.Repositories
{
    public class InvitationRepository
    {
        private readonly AppDbContext _context;

        public InvitationRepository(AppDbContext context)
        {
            _context = context;
        }

        public InvitationCode Create(InvitationCode invitation)
        {
            _context.Set<InvitationCode>().Add(invitation);
            _context.SaveChanges();
            return invitation;
        }

        public InvitationCode? GetByCode(string code)
        {
            return _context.Set<InvitationCode>()
                .FirstOrDefault(i => i.Code == code);
        }

        public void Update(InvitationCode invitation)
        {
            _context.Set<InvitationCode>().Update(invitation);
            _context.SaveChanges();
        }

        public bool Exists(string code)
        {
            return _context.Set<InvitationCode>().Any(i => i.Code == code);
        }

        public void Delete(int id)
        {
            var invitation = _context.Set<InvitationCode>().Find(id);
            if (invitation == null)
                throw new BusinessException("Invitation not found");

            _context.Set<InvitationCode>().Remove(invitation);
            _context.SaveChanges();
        }
        public void MarkAsUsed(string code)
        {
            var invitation = _context.Set<InvitationCode>()
                .FirstOrDefault(i => i.Code == code);

            if (invitation == null)
                throw new BusinessException("Invitation not found");

            if (invitation.IsUsed)
                throw new BusinessException("Invitation already used");

            if (invitation.ExpiresAt < DateTime.UtcNow)
                throw new BusinessException("Invitation expired");

            invitation.IsUsed = true;
            _context.SaveChanges();
        }
        public bool IsValid(string code)
        {
            var invitationCode = GetByCode(code);

            if (invitationCode == null)
                throw new BusinessException("Código de invitación no existe");

            if (invitationCode.IsUsed)
                throw new BusinessException("El código ya fue utilizado");

            if (invitationCode.ExpiresAt < DateTime.UtcNow)
                throw new BusinessException("El código ha expirado");

            return true;
        }
    }
}
