using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransitHubRepo;
using TransitHubRepo.Dto;
using TransitHubRepo.Models;
using Microsoft.AspNetCore.SignalR;
namespace TransitHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalTripController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public LocalTripController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllLocalTrips")]
        public IActionResult Get()
        {
            var total = _unitOfWork.Transports.GetAll();
            return Ok(total);
        }
        [HttpPost("CreateLocalTrip")]
        public IActionResult CreateOne(LocalTripCreateDto local) 
        {
            LocalTransport t = new LocalTransport();
            t.From = local.From;
            t.To = local.To;
            t.VichelType = local.VichelType;
            t.UserId = local.UserId;
            t.Position = local.Position;
            var result = _unitOfWork.Transports.Create(t);
            _unitOfWork.Commit();
            return Accepted(result);
        }
    }
}
