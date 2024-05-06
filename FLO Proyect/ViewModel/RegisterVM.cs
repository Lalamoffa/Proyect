using System.ComponentModel.DataAnnotations;

namespace FLO_Proyect.ViewModel
{
    public class RegisterVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPasword { get; set; }
        public bool IsRemember { get; set; }
    }
}
