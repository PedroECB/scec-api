using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCEC.API.Models.DTO
{
    public class LoginDTO
    {
        [Required]
        [MinLength(10, ErrorMessage = "Endereço de e-mail inválido, insira a quantidade mínima de caracteres")]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
