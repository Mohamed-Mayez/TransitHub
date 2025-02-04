using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitHubRepo.Dto
{
    public class LocalTripCreateDto
    {
        [Required]
        public string? From { get; set; }
        [Required]
        public string? To { get; set; }
        [Required]
        public string? VichelType { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        public string? Position { get; set;}
    }
}
