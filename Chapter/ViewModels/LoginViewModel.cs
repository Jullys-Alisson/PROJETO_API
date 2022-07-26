using System.ComponentModel.DataAnnotations;

namespace Chapter.ViewModels
{
    public class LoginViewModel
    {
        [Required (ErrorMessage = "Informe o E-mail")]
        public string Email  { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        public string Senha { get; set; }
    }
}
