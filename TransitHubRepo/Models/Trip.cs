using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitHubRepo.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? startLocation { get; set; }
        [Required]
        [MaxLength(100)]
        public string? endLocation { get; set; }
        [Required]
        public DateTime? dateOfTrip { get; set; }
        [Required]
        [MaxLength(30)]
        public string? viechelType { get; set;}


        public string? userId { get; set; }
        [ForeignKey("userId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }

    }
}
