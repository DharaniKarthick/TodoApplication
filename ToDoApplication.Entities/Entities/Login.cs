using System.ComponentModel.DataAnnotations;

namespace TodoApplication.Entities
{
    public class Login
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
