using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace SCEC.API.Models
{
    [Table("roles", Schema = "public")]
    public class Role
    {
        [Key]
        [Column("id_role")]
        public long Id { get; set; }

        [Column("role")]
        public string RoleDescription{ get; set; }

        [Column("alias")]
        public string Alias { get; set; }

        [Column("enabled")]
        public string Enabled { get; set; }
    }
}
