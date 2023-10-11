using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCEC.API.Models.DTO
{
    public class ResponseDataDTO
    {
        public int CodRetorno { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ResponseDataDTO()
        {
        }

        public ResponseDataDTO(string message)
        {
            CodRetorno = 0;
            Message = message;
        }

        public ResponseDataDTO(int codRetornno, string message = null)
        {
            CodRetorno = codRetornno;
            Message = message;
        }

        public ResponseDataDTO(int codRetornno, string message = null, object data = null)
        {
            CodRetorno = codRetornno;
            Message = message;
            Data = data;
        }
    }
}
