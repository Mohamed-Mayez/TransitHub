using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitHubEFCore.Repositories;
using TransitHubRepo;
using TransitHubRepo.Interfaces;
using TransitHubRepo.Models;



namespace TransitHubEFCore
{
    // here i will set the type of each interface and initailize it
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<Trip> Trips {  get; private set; }

        public IBaseRepository<LocalTransport> Transports { get; private set; }

        public IBaseRepository<UserImage> UserImages {  get; private set; }
        public IBaseRepository<UserConnection> Connections { get; private set; }
        public IBaseRepository<Message> Messages { get; private set; }
        public IBaseRepository<Rate> Rates { get; private set; }

        public IQRCodeRepository QRCode {  get; private set; }
        

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Trips = new BaseRepository<Trip>(_context);
            Transports = new BaseRepository<LocalTransport>(_context);
            UserImages = new BaseRepository<UserImage>(_context);
            QRCode = new QRCodeRepository(_context);
            Connections = new BaseRepository<UserConnection> (_context);
            Messages = new BaseRepository<Message>(_context);
            Rates = new BaseRepository<Rate>(_context);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
