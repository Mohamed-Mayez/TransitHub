using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitHubRepo.Dto
{
    public class TripCreateDto
    {
        [Required]
        public string? UserId { get; set; }
        [Required]
        public string? startLocation { get; set; }
        [Required]
        public string? endLocation { get; set; }
        [Required]
        public DateTime? dateOfTrip { get; set; }
        [Required]
        public string? viechelType { get; set; }
    }
}
