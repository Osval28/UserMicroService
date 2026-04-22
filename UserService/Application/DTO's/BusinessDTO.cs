using UserService.Domain.Model;

namespace UserService.Application.DTOs
{
    public class BusinessDTO
    {
        public string Name { get; set; } = "";
        public string NIT { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";

        public Business FromDTOtoModel()
        {
            return new Business
            {
                Name = Name,
                NIT = NIT,
                Address = Address,
                Phone = Phone,
                Email = Email
            };
        }
    }
}