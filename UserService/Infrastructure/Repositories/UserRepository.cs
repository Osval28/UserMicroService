using Microsoft.EntityFrameworkCore;
using UserService.Application.Exceptions;

namespace UserService.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public UserEntity CreateUser(UserEntity user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool ExistsByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool ExistsByUsername(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }

        public bool ExistsByPhone(string phone)
        {
            return _context.Users.Any(u => u.Phone == phone);
        }

        public UserEntity? GetByUsernameOrEmailWithRole(string identificador)
        {
            return _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == identificador || u.Email == identificador);
        }

        public void AssignBusiness(int userId, int businessId, int roleId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new BusinessException("User not found");

            user.BusinessId = businessId;
            user.RoleId = roleId;
            _context.SaveChanges();
        }
    }
}