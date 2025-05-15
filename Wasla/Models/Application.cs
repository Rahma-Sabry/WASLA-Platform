using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Wasla.Models
{
    [Table(name: "Applications")]
    public class Application
    {
        public int EmployeeId { get; set; }

        public int JobId { get; set; }

        public DateTime ApplyDate { get; set; } = DateTime.Now;

        [ValidateNever]
        public Employee Employee { get; set; }

        [ValidateNever]
        public Job Job { get; set; }
    }

}
