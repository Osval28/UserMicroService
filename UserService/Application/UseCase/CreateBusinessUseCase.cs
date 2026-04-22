using UserService.Application.Exceptions;
using UserService.Domain.Model;
using UserService.Infrastructure;
using UserService.Infrastructure.Repositories;

namespace UserService.Application.UseCase
{
    public class CreateBusinessUseCase
    {
        private readonly BusinessRepository _businessRepository;

        public CreateBusinessUseCase(BusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }

        public BusinessEntity Execute(Business business)
        {
            if (_businessRepository.ExistsByName(business.Name))
                throw new BusinessException("A business with that name already exists");

            var newBusinessDB = new BusinessEntity
            {
                Name = business.Name,
                NIT = business.NIT,
                Address = business.Address,
                Phone = business.Phone,
                Email = business.Email,
                AdminUserId = business.AdminUserId
            };

            return _businessRepository.CreateBusiness(newBusinessDB);
        }
    }
}