using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wasla.Models
{
    [Table(name: "Applications")]
    public class Application
    {
        public int EmployeeId { get; set; }

        public int JobId { get; set; }

        public DateTime ApplyDate { get; set; } = DateTime.Now;

        public Employee Employee { get; set; }

        public Job Job { get; set; }
    }

}
