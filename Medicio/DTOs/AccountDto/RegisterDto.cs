using System.ComponentModel.DataAnnotations;

namespace Medicio.DTOs.AccountDto
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name {  get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Surname {  get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Username {  get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword {  get; set; }
    }
}
