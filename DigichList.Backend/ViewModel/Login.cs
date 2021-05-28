using System.ComponentModel.DataAnnotations;

namespace DigichList.Backend.ViewModel
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
