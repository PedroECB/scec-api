using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace SCEC.API.Models
{
    [Table("modules", Schema = "public")]
    public class Module
    {
        [Key]
        [Column("id_module")]
        public long IdModule { get; set; }

        [ForeignKey("Module")]
        [Column("id_parent")]
        public long? ParentId { get; set; }

        [Column("description")]
        public string Description{ get; set; }

        [Column("alias_flag")]
        public string AliasFlag { get; set; }

        [Column("url_base")]
        public string UrlBase { get; set; }

        [Column("enabled")]
        public string Enabled { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("order")]
        public int Order { get; set; }

        [Column("icon")]
        public string Icon { get; set; }

        [NotMapped]
        public List<Module> Nested { get; set; }

    }
}
