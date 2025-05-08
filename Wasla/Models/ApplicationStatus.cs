using System.ComponentModel.DataAnnotations;

namespace Wasla.Models
{
    public class ApplicationStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string? StatusName { get; set; }
        // Pending, Accepted, Rejected, Interviewing
    }
}
