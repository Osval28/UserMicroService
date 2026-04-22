using UserService.Infrastructure;

namespace UserService.Infrastructure.Repositories
{
    public class BusinessRepository
    {
        private readonly AppDbContext _context;

        public BusinessRepository(AppDbContext context)
        {
            _context = context;
        }

        public BusinessEntity CreateBusiness(BusinessEntity business)
        {
            _context.Businesses.Add(business);
            _context.SaveChanges();
            return business;
        }

        public bool ExistsById(int id)
        {
            return _context.Businesses.Any(b => b.Id == id);
        }

        public bool ExistsByName(string name)
        {
            return _context.Businesses.Any(b => b.Name == name);
        }
    }
}