using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitHubRepo.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? MessageContent { get; set; }
        [Required]
        public DateTime Time { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public string? Sender { get; set; }
        public string? Recever { get; set;}

        [ForeignKey("Sender")]
        public virtual ApplicationUser? ApplicationUserSender { get; set; }
        [ForeignKey("Recever")]
        public virtual ApplicationUser? ApplicationUserResever { get; set; }

    }
}
