using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace SCEC.API.Models
{
    [Table("users", Schema = "public")]
    public class User
    {
        [Key]
        [Column("id_user")]
        public int Id { get; set; }

        [Column("uuid")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Uuid { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("salt")]
        public string Salt { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("enabled")]
        public string Enabled { get; set; }

        [NotMapped]
        public string Roles { get; set; }
    }
}
