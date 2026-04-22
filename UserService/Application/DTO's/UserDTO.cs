using UserService.Domain.Model;

namespace UserService.Application.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public RegisterType RegisterType { get; set; }

        public User FromDTOtoModel()
        {
            return new User
            {
                Name = Name,
                LastName = LastName,
                Phone = Phone,
                Username = Username,
                Email = Email,
                Password = Password,
                RegisterType = RegisterType,
                BusinessId = null
            };
        }
    }
}