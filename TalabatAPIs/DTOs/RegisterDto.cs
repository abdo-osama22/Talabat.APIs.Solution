using System.ComponentModel.DataAnnotations;

namespace TalabatAPIs.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName {  get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&amp;*()_+]).*$",
            ErrorMessage = "Password Must Contain 1 of (Uppercase ,LowerCase ,Digit ,Spesial character")]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
