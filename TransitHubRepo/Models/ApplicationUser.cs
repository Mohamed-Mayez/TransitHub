using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TransitHubRepo.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string? CardNumberId { get; set; }
        public string? DateOfBirth { get; set; }
        public string? ImgUrl { get; set; }
        public int NumberOfTrips { get; set; }
        public string? CurrentPassword {  get; set; }


      
        public virtual UserImage? Image { get; set; }
    }
}
