using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitHubRepo.Dto
{
    public class TripWithUserDto
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? ViechelType { get; set; }
        public string? TripNumber { get; set; }
        public string? PersonalImgUrl { get; set; }
        public DateTime? TripTime { get; set; }
    }
}
