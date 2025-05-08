using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Wasla.Models
{
    public class Skill
    {
        [Key]
        public int SkillId { get; set; }

        [StringLength(20)]
        [Display(Name = "Skill Name")]
        public string SkillName { get; set; }

        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }

}
