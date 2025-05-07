using System.ComponentModel.DataAnnotations;

namespace Wasla.Models
{
    public class JobType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string TypeName { get; set; }
        // Full-Time, Part-Time, Internship, Remote
    }
}
