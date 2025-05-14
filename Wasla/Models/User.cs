using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Wasla.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        
        [StringLength(maximumLength: 15)]
        public string? FirstName { get; set; }


        [StringLength(maximumLength: 15)]
        public string? LastName { get; set; }

        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName => FirstName + " " + LastName;

        [StringLength(maximumLength: 15)]
        public string? SSN { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [NotMapped]
        public int Age => DateTime.Now.Year - DateOfBirth.Year;

        public int Coins { get; set; } = 50;
    }

}
