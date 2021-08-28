
using System.ComponentModel.DataAnnotations;

namespace TaskManager.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email address is empty")]
        public string EmailAddress;
        [Required(ErrorMessage = "Password is empty")]
        public string Password;
    }
}
