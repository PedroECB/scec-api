using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCEC.API.Models.DTO
{
    public class AddUserDTO
    {
        [Required]
        [MaxLength(150, ErrorMessage = "O endereço de e-mail não pode ter mais que 150 caracrteres")]
        public string Email { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "O nome do usuário não pode ter mais que 100 caracrteres")]
        public string Name { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
