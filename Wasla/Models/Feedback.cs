using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wasla.Models
{
    public class Feedback
    {
        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;

        public int JobId { get; set; }

        public int CurrentStatusId { get; set; }

        public User Sender { get; set; }

        public User Receiver { get; set; }

        public Job Job { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }
    }

}
