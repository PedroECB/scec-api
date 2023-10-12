using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace SCEC.API.Models
{
    [Table("email_models", Schema = "public")]
    public class EmailModel
    {
        [Key]
        [Column("id_email_model")]
        public long IdEmailModel { get; set; }

        [Column("flag")]
        public string Flag{ get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("text")]
        public string Text { get; set; }

        [Column("subject")]
        public string Subject { get; set; }
        
    }
}
