using LibraryDataAccess.Entities.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDataAccess.Entities
{
    [Table("Users")]
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}