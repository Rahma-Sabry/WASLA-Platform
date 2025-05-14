using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Wasla.Models
{
    public class CoinHistory
    {
        public int UserId { get; set; }

        public DateTime Time { get; set; }

        public int? ProcessTypeId { get; set; }

        public int? Amount { get; set; }
        
        public User User { get; set; }
        
        public ProcessType ProcessType { get; set; }
    }

}
