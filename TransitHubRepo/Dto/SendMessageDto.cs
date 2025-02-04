using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitHubRepo.Dto
{
    public class SendMessageDto
    {
        [Required]
        public string? MessageContent { get; set; }
        [Required]
        public string? SenderId { get; set; }
        [Required]
        public string? ReseverId { get; set; }
    }
}
