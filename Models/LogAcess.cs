using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace SCEC.API.Models
{
    [Table("log_acess", Schema = "public")]
    public class LogAcess
    {
        [Key]
        [Column("id_log_acess")]
        public long IdLogAcess { get; set; }

        [Column("id_user")]
        [ForeignKey("User")]
        public long IdUser { get; set; }

        public User User { get; set; }

        [Column("dt_acess")]
        public DateTime DtAcess { get; set; }

        [Column("ip")]
        public string Ip { get; set; }

        public LogAcess(long idUser, string ip = null)
        {
            this.IdUser = idUser;
            this.Ip = ip;
            this.DtAcess = DateTime.Now;
        }
    }
}
