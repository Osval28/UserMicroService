using UserService.Application.DTOs;
using UserService.Application.UseCase;
using UserService.Infrastructure;

namespace UserService.Application.Services
{
    public class BusinessService
    {
        private readonly CreateBusinessUseCase _createBusinessUseCase;

        public BusinessService(CreateBusinessUseCase createBusinessUseCase)
        {
            _createBusinessUseCase = createBusinessUseCase;
        }

        public void CreateBusiness(BusinessDTO dto, int adminUserId)
        {
            var business = dto.FromDTOtoModel();
            business.AdminUserId = adminUserId;
            _createBusinessUseCase.Execute(business);
        }
    }
}