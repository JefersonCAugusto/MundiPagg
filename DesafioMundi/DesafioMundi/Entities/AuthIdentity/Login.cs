using System.ComponentModel.DataAnnotations;

namespace DesafioMundi.Entities.AuthIdentity
{
    public class Login
    {

        [EmailAddress(ErrorMessage = "Formato inválido em {0}")]
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        public string Email { get; set; }
       
        [StringLength(50, ErrorMessage ="{0} precisa ter entre {2} e {1} caracteres"), MinLength(6)]
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        public string Password { get; set; }
    }
}
