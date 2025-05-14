using System.ComponentModel.DataAnnotations;

namespace Wasla.Models
{
    public class RegisterViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name Can't be Empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Can't be Empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name Can't be Empty")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "SSN is Required")]
        [Display(Name = "National Id")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "National Id must be exactly 13 digits.")]
        public string? SSN { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }


        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please Provide Mail")]
        [EmailAddress(ErrorMessage = "Invalid E-Mail Format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Confirm Your Password")]
        [Required(ErrorMessage = "Please Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Not Matced")]
        public string? ConfirmPassword { get; set; }

        public bool isRecruiter { get; set; }
    }

    public class LoginViewModel
    {
        public int Id { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name Can't be Empty")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool Remember { get; set; }
    }
}
