using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitHubRepo.Models
{
    public class Shipp
    {
        public int Id { get; set; }
        public string? Name { get; set; }
       

        public int? TripId { get; set; }
        [ForeignKey("TripId")]
        public virtual Trip? Trip { get; set; }

        public int? LocalTransportId { get; set; }
        [ForeignKey("LocalTransportId")]
        public virtual LocalTransport? LocalTransport { get; set; }
    }
}
