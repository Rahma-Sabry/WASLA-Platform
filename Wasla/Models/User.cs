using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wasla.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First Name Can't be Empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        [Display(Name = "First Name")]
        [StringLength(maximumLength: 15)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Can't be Empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        [Display(Name = "Last Name")]
        [StringLength(maximumLength: 15)]
        public string LastName { get; set; }

        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName => FirstName + " " + LastName;

        [Required(ErrorMessage = "Email is a Required Field")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        
        public string Email { get; set; }

        [Required(ErrorMessage = "SSN is Required")]
        [Display(Name = "National Id")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "National Id must be exactly 13 digits.")]
        [StringLength (maximumLength: 15)]
        public string SSN { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Only numeric digits are allowed.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password Can't be Empty")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password Can't be Empty")]
        [Compare(nameof(Password), ErrorMessage = "Password doesn't match")]
        [NotMapped]
        public string ValidatePassword { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [NotMapped]
        public int Age => DateTime.Now.Year - DateOfBirth.Year;

        public int Coins { get; set; } = 50;
    }

}
