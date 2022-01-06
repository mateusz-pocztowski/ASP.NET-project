using System.ComponentModel.DataAnnotations;

namespace BooksMVC.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Użytkownik")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberLogin { get; set; }

        public string ReturnUrl { get; set; }

    }
}
