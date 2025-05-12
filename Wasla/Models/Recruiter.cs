using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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
        [ValidateNever]
        public ICollection<Job> Jobs { get; set; }
    }

}
