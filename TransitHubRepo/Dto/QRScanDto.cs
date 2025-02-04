using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitHubRepo.Dto
{
    public class QRScanDto
    {
        [Required]
        public string? QRcode { get; set; }
        [Required]
        public string? CarrierId { get; set; }
    }
}
