using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitHubRepo.Dto
{
    public class UserRateDto
    {
        [Required]
        public string? UserId { get; set; }
        public string? OneStar { get; set; }
        public string? TwoStars { get; set; }
        public string? ThreeStars { get; set; }
        public string? FourStars { get; set; }
        public string? FiveStars { get; set; }
    }
}
