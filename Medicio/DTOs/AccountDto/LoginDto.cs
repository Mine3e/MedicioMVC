using System.ComponentModel.DataAnnotations;

namespace Medicio.DTOs.AccountDto
{
    public class LoginDto
    {
        [Required]
        public string UserNameOrEmail {  get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsRemembered {  get; set; }
    }
}
