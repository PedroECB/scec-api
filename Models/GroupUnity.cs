using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace SCEC.API.Models
{
    [Table("group_unity", Schema = "public")]
    public class GroupUnity
    {
        [Key]
        [Column("id_group_unity")]
        public long Id { get; set; }

        [Column("unity_name")]
        public string UnityName { get; set; }

        [Column("alias_name")]
        public string AliasName { get; set; }

        [Column("group_code")]
        public string GroupCode { get; set; }

        [Column("id_address")]
        [ForeignKey("Address")]
        public long IdAdress { get; set; }
        public Address Address { get; set; }

        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedAt { get; set; }
        
        [Column("enabled")]
        public char Enabled { get; set; }
    }
}
