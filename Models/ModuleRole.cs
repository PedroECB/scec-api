using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace SCEC.API.Models
{
    [Table("modules_roles", Schema = "public")]
    public class ModuleRole
    {
        [Key]
        [Column("id_modules_roles")]
        public long IdModuleRole { get; set; }

        [ForeignKey("Module")]
        [Column("id_module")]
        public long IdModule { get; set; }
        public Module Module { get; set; }

        [ForeignKey("Role")]
        [Column("id_role")]
        public long Idrole { get; set; }
        public Role Role { get; set; }

    }
}
