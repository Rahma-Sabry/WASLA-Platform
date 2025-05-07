using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Wasla.Models
{
    
    public class ApplicationHistory
    {
        [Key]
        public int EmployeeId { get; set; }

        public int JobId { get; set; }

        public DateTime ChangeDate { get; set; } = DateTime.Now;

        public Employee Employee { get; set; }

        public Job Job { get; set; }

        public ApplicationStatus Previous { get; set; }

        public ApplicationStatus Current { get; set; }
    }

}
