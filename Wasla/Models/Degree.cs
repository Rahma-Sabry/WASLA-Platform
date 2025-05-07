using System.ComponentModel.DataAnnotations;

namespace Wasla.Models
{
    public class Degree
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string DegreeName { get; set; }
        // BS, MS or PhD
    }
}
