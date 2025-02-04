using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitHubRepo.Dto;
using TransitHubRepo.Interfaces;
using TransitHubRepo.Models;

namespace TransitHubEFCore.Repositories
{
    public class QRCodeRepository : BaseRepository<OrderQR>, IQRCodeRepository
    {
        // private new readonly ApplicationDbContext? context;
        public QRCodeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public bool CheackScan(string QRcode, string senderId)
        {
            OrderQR? model = _context?.orderQRs?.FirstOrDefault(c => c.SenderId == senderId && c.QRCode == QRcode);
            if (model != null)
            {
                return model.Scaned;
            }
            return false;
        }

        public bool ScanQR(string code,string id)
        {
            OrderQR? model = _context?.orderQRs?.FirstOrDefault(c =>  c.CarrierId == id && c.QRCode == code);
            if (model != null)
            {
                if(model.Scaned == false)
                {
                    model.Scaned = true;
                    _context?.orderQRs?.Update(model);
                    return true;
                }
            }
            return false;
        }
    }
}
