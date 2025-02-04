using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitHubRepo.Interfaces;
using TransitHubRepo.Models;

namespace TransitHubRepo
{
    // this interface that i will put all raferances of interfaces i use 
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Trip> Trips { get; }
        IBaseRepository<LocalTransport> Transports { get; }
        IBaseRepository<UserImage> UserImages { get; }
        IBaseRepository<UserConnection> Connections { get; }
        IBaseRepository<Message> Messages { get; }
        IQRCodeRepository QRCode { get; }
        IBaseRepository<Rate> Rates { get; }
        

        int Commit();
    }
}
