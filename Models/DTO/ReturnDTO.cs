using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCEC.API.Models.DTO
{
    public class ReturnDTO
    {
        public int CodRetorno { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ReturnDTO()
        {

        }
    }
}
