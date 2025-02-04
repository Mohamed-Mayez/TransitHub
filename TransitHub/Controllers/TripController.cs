using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.AccessControl;
using TransitHubRepo;
using TransitHubRepo.Dto;
using TransitHubRepo.Interfaces;
using TransitHubRepo.Models;

namespace TransitHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class TripController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public TripController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        [HttpGet("GetAllTrips")]
        public IActionResult GetByAll()
        {
            var t = _unitOfWork.Trips.GetAll();
            return Ok(t);
        }
        [HttpPost("AddTrip")]
        public async Task <IActionResult> CreateTrip(TripCreateDto trip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByIdAsync(trip.UserId);
            if (user == null)
            {
                return BadRequest("Use Id is incorrect");
            }
            Trip t = new Trip();
            t.startLocation = trip.startLocation;
            t.endLocation = trip.endLocation;
            t.viechelType = trip.viechelType;
            t.dateOfTrip = trip.dateOfTrip;
            t.userId = trip.UserId;
            _unitOfWork.Trips.Create(t);
            _unitOfWork.Commit();
            return Accepted(t);
        }
        [HttpGet("gettripwithuser")]
        public IActionResult GetTripWithUser()
        {
            List<TripWithUserDto> tripWithUserDto = new List<TripWithUserDto>();
            var all = _unitOfWork.Trips.FindAll(new[] { "ApplicationUser" });
            foreach (var trip in all)
            {

                var im = _unitOfWork.UserImages.FindOne(b => b.UserId == trip.userId);
                if (im != null)
                {
                    tripWithUserDto.Add(new TripWithUserDto()
                    {
                        UserId = trip.userId,
                        UserName = trip.ApplicationUser?.UserName,
                        From = trip.startLocation,
                        To = trip.endLocation,
                        ViechelType = trip.viechelType,
                        TripNumber = Convert.ToString(trip.ApplicationUser?.NumberOfTrips),
                        TripTime = (DateTime)trip.dateOfTrip!,
                        PersonalImgUrl = im.Url
                    });
                }
                else
                {
                    tripWithUserDto.Add(new TripWithUserDto()
                    {
                        UserId = trip.userId,
                        UserName = trip.ApplicationUser?.UserName,
                        From = trip.startLocation,
                        To = trip.endLocation,
                        ViechelType = trip.viechelType,
                        TripNumber = Convert.ToString(trip.ApplicationUser?.NumberOfTrips),
                        TripTime = (DateTime)trip.dateOfTrip!,
                        PersonalImgUrl = null
                    });
                }
                
            }
            return Ok(tripWithUserDto);
        }
        [HttpGet("searchtrip")]
        public IActionResult Sreach(string from,string to)
        {
            List<TripWithUserDto> tripWithUserDto = new List<TripWithUserDto>();
            var result = _unitOfWork.Trips.FindAll(t => t.startLocation!.ToUpper().Contains(from.ToUpper()) && t.endLocation!.ToUpper().Contains(to.ToUpper()), new[] { "ApplicationUser" });
            if(!result.IsNullOrEmpty()) 
            {
                foreach (var trip in result)
                {
                    var im = _unitOfWork.UserImages.FindOne(b => b.UserId == trip.userId);
                    if (im != null)
                    {
                        tripWithUserDto.Add(new TripWithUserDto()
                        {
                            UserId = trip.userId,
                            UserName = trip.ApplicationUser?.UserName,
                            From = trip.startLocation,
                            To = trip.endLocation,
                            ViechelType = trip.viechelType,
                            TripNumber = Convert.ToString(trip.ApplicationUser?.NumberOfTrips),
                            TripTime = (DateTime)trip.dateOfTrip!,
                            PersonalImgUrl = im.Url
                        });
                    } 
                    else
                    {
                         tripWithUserDto.Add(new TripWithUserDto()
                         {
                            UserId = trip.userId,
                            UserName = trip.ApplicationUser?.UserName,
                            From = trip.startLocation,
                            To = trip.endLocation,
                            ViechelType = trip.viechelType,
                            TripNumber = Convert.ToString(trip.ApplicationUser?.NumberOfTrips),
                            TripTime = (DateTime)trip.dateOfTrip!,
                            PersonalImgUrl = null
                        });
                    }
                    
                }
                return Ok(tripWithUserDto);
            }
            else 
            {
                return BadRequest("Not found");
            }
        }
    }
}
