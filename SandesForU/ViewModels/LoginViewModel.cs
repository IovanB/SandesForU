using System.ComponentModel.DataAnnotations;

namespace SandesForU.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Usuário obrigatório")]
        [Display(Name = "Usuário")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Senha obrigatório")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
