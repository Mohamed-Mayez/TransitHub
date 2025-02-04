using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitHubRepo.Models
{
    public class Rate
    {
        [Key]
        public int RateId { get; set; }
        public string? OneStar { get; set; }
        public string? TwoStars { get; set; }
        public string? ThreeStars { get; set; }
        public string? FourStars { get; set; }
        public string? FiveStars { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
    }
}
