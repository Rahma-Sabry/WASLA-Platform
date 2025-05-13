using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Wasla.Models
{
    public class Education
    {
        public int EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "University Name Can't be Empty")]
        [Display(Name = "University Name")]
        [StringLength(maximumLength: 50)]
        public string? UniversityName { get; set; }

        public DateTime? EndDate { get; set; }

        public int DegreeId { get; set; }

        [ValidateNever]
        public Employee Employee { get; set; }

        [ValidateNever]
        public Degree DegreeType { get; set; }
    }

}
