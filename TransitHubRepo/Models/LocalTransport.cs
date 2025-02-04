using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitHubRepo.Models
{
    public class LocalTransport
    {
        public int Id { get; set; }
        [Required]
        public string? From { get; set; }
        [Required]
        public string? To { get; set; }
        [Required]
        public string? VichelType {  get; set; }
        [Required]
        public string? Position { get; set; }
        [Required]
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        public ICollection<Shipp>? Shipps { get; set; }
    }
}
