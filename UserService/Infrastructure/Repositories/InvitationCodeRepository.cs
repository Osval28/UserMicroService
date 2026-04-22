using UserService.Infrastructure;

namespace UserService.Infrastructure.Repositories
{
    public class InvitationCodeRepository
    {
        private readonly AppDbContext _context;

        public InvitationCodeRepository(AppDbContext context)
        {
            _context = context;
        }

        public InvitationCode CreateCode(InvitationCode invitationCode)
        {
            _context.InvitationCodes.Add(invitationCode);
            _context.SaveChanges();
            return invitationCode;
        }

        public InvitationCode? GetByCode(string code)
        {
            return _context.InvitationCodes
                .FirstOrDefault(i => i.Code == code);
        }

        public void MarkAsUsed(InvitationCode invitationCode)
        {
            invitationCode.IsUsed = true;
            _context.SaveChanges();
        }
    }
}