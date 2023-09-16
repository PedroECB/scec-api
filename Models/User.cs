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
        public string Uuid { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

    }
}
