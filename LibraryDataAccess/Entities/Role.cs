using LibraryDataAccess.Entities.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDataAccess.Entities
{
    [Table("Roles")]
    public class Role : Entity
    {
        public string Name { get; set; }
    }
}
