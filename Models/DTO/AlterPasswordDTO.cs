using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCEC.API.Models.DTO
{
    public class AlterPasswordDTO
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "A senha deve conter no máximo 100 caracteres")]
        [MinLength(5, ErrorMessage = "A senha deve conter no mínimo 5 caracteres!")]
        public string NewPassword { get; set; }

    }
}
