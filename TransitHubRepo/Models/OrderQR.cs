using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitHubRepo.Models
{
    public class OrderQR
    {
        [Key]
        public int Id { get; set; }
        public bool Scaned { get; set; } 
        public bool Created { get; set; }

        public string? QRCode { get; set; }
        
        public string? SenderId { get; set;}
        public string? Price { get; set; }
       
        public string? CarrierId { get; set; }
        [ForeignKey("SenderId")]
        public virtual ApplicationUser? ApplicationUserSender { get; set; }
        [ForeignKey("CarrierId")]
        public virtual ApplicationUser? ApplicationUserCarrier { get; set; }
    }
}
