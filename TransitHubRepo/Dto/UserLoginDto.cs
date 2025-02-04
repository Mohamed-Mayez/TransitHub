using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitHubRepo.Dto
{
    public class UserLoginDto
    {
        [Required]
        public string? UserEmail { get; set;}
        [Required]
        public string? Password { get; set;}
    }
}
