using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wasla.Models
{
    public class Skill
    {
        public int EmployeeId { get; set; }

        [StringLength(20)]
        [Display(Name = "Skill Name")]
        public string SkillName { get; set; }

        public Employee Employee { get; set; }
    }

}
