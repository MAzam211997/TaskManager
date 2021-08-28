using System.ComponentModel.DataAnnotations;

namespace TaskManager.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Full Name is required")]
        public string Fullname;
        [Required(ErrorMessage = "Email is required")]
        public string Email;
        [Required(ErrorMessage = "Password is required")]
        public string Password;
        [Required(ErrorMessage = "Role is required.")]
        public int Role;
        [Required(ErrorMessage = "Mobile number is required")]
        public string Mobile;
        public bool IsActive;
    }
}
