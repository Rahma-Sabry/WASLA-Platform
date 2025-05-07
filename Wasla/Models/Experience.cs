using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wasla.Models
{
    public class Experience
    {
        public int EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Company Name Can't be Empty")]
        [Display(Name = "Company Name")]
        [StringLength(maximumLength: 50)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Title Can't be Empty")]
        [Display(Name = "Title")]
        [StringLength(maximumLength: 50)]
        public string PositionTitle { get; set; }

        public DateTime? EndDate { get; set; }

        public Employee Employee { get; set; }
    }

}
