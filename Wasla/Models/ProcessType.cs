using System.ComponentModel.DataAnnotations;

namespace Wasla.Models
{
    public class ProcessType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string ProcessName { get; set; }
        // Withdraw or Deposit
    }
}
