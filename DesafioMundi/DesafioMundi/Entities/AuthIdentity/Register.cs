using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMundi.Entities.AuthIdentity
{
    public class Register
    {
        [EmailAddress(ErrorMessage = "Formato inválido em {0}")]
        [Required(ErrorMessage ="Campo {0} é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; }

    }
}
 