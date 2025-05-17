using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace SCEC.API.Models
{
    [Table("address", Schema = "public")]
    public class Address
    {
        [Key]
        [Column("id_address")]
        public long Id { get; set; }

        [Column("street")]
        public string Street { get; set; }

        [Column("number")]
        public int? Number { get; set; }

        [Column("complement")]
        public string Complement { get; set; }
        
        [Column("city")]
        public string City { get; set; }

        [Column("uf_state")]
        public string UfState{ get; set; }

        [Column("zipcode")]
        public string zipcode { get; set; }

        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedAt { get; set; }

    }
}
