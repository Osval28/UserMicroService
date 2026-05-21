using Microsoft.EntityFrameworkCore;
using UserService.Domain.Model;

namespace UserService.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<InvitationCode> InvitationCodes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "StoreAdmin" },
                new Role { Id = 2, Name = "Assistant" },
                new Role { Id = 3, Name = "NaturalPerson" },
                new Role { Id = 4, Name = "PlatformAdmin" },
                new Role { Id = 5, Name = "Moderator" }
            );
        }
    }

    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string LastName { get; set; } = "";
        public RegisterType RegisterType { get; set; }
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public int RoleId { get; set; }
        public Role Role { get; set; } = null;
        public int? BusinessId { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }

    public class InvitationCode
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";     
        public int BusinessId { get; set; }
        public int RoleId { get; set; }            
        public DateTime ExpiresAt { get; set; }   
        public bool IsUsed { get; set; } = false;  
        public int GetRoleId()
        {
            return RoleId;
        }
        public int GetBusinessId()
        {
            return BusinessId;
        }
        public string GetCode()
        {
            return Code;
        }
    }

}