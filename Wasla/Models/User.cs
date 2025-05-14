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
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }


        [StringLength(maximumLength: 15)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName => FirstName + " " + LastName;

        [StringLength(maximumLength: 15)]
        [Display(Name = "Identity Number")]
        public string? SSN { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime DateOfBirth { get; set; }

        [NotMapped]
        public int Age => DateTime.Now.Year - DateOfBirth.Year;

        public int Coins { get; set; } = 50;
    }

}
