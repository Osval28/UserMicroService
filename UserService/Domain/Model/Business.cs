namespace UserService.Domain.Model
{
    public class Business
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string NIT { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public int AdminUserId { get; set; }
    }
}