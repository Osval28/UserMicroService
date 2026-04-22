namespace UserService.Domain.Model
{
    public class User
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RegisterType RegisterType { get; set; }
        public int? BusinessId { get; set; }
        public int RoleId { get; set; }
    }
}   