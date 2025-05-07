using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wasla.Models
{
    public class Education
    {
        public int EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "University Name Can't be Empty")]
        [Display(Name = "University Name")]
        [StringLength(maximumLength: 50)]
        public string UniversityName { get; set; }

        public DateTime? EndDate { get; set; }

        public int DegreeId { get; set; }

        public Employee Employee { get; set; }

        public Degree DegreeType { get; set; }
    }

}
