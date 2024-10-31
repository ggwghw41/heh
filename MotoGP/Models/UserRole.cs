using System.ComponentModel.DataAnnotations;

namespace MotoGP.Models
{
    public class UserRole
    {
        [Key]
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        [Key]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
