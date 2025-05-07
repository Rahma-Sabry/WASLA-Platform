using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wasla.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        [Required]
        public int RecruiterId { get; set; }

        [Display(Name = "Job Title")]
        [StringLength(50)]
        public string JobTitle { get; set; }

        public string Requirements { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Salaray must be Greater than 0")]
        public decimal Salary { get; set; }

        public int TypeId { get; set; }

        [MaxLength(20)]
        public string Field { get; set; }

        public DateTime? ExpireDate { get; set; }

        public Recruiter Recruiter { get; set; }

        public JobType JobType { get; set; }

        public ICollection<Application> Applications { get; set; }
    }

}
