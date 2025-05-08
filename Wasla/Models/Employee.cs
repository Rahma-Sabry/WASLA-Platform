using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wasla.Models
{
    public class Employee : User
    {

        [Url]
        public string? Resume { get; set; }

        [Url]
        public string? CoverLetter { get; set; }

        public ICollection<Education> EducationRecords { get; set; }
        public ICollection<Experience> Experiences { get; set; }
        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
        public ICollection<Application> Applications { get; set; }
    }

}
