using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wasla.Models
{
    public class Recruiter : User
    {
        [Required(ErrorMessage = "Company Name Can't be Empty")]
        [Display(Name = "Company Name")]
        [StringLength(maximumLength: 50)]
        public string CompanyName { get; set; }

        [Display(Name = "Company Description")]
        public string CompanyDesc { get; set; }

        public ICollection<Job> Jobs { get; set; }
    }

}
