using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SCEC.API.Models
{
    [Table("users_roles")]
    public class UserRole
    {
        [Key]
        [Column("id_user_role")]
        public long Id { get; set; }

        [ForeignKey("User")]
        [Column("id_user")]
        public long IdUser { get; set; }

        [ForeignKey("Role")]
        [Column("id_role")]
        public long IdRole { get; set; }

        [Column("enabled")]
        public string Enabled { get; set; }

        public Role Role { get; set; }

    }
}
