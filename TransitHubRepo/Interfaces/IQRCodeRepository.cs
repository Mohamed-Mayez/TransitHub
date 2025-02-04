using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitHubRepo.Dto;
using TransitHubRepo.Models;

namespace TransitHubRepo.Interfaces
{
    public interface IQRCodeRepository : IBaseRepository<OrderQR>
    {
       
        bool ScanQR(string code, string id);
        bool CheackScan(string QRcode , string senderId);
    }
}
