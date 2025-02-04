using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransitHubRepo;
using TransitHubRepo.Dto;
using TransitHubRepo.Models;

namespace TransitHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RateController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("addrate")]
        public IActionResult AddRate(UserRateDto rate)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest($"Please Put User Id");
            }
            var userRate = _unitOfWork.Rates.FindOne(r => r.UserId == rate.UserId);
            if(userRate == null)
            {
                var newUserRate = new Rate();
                newUserRate.UserId = rate.UserId;
                newUserRate.OneStar = rate.OneStar;
                newUserRate.TwoStars = rate.TwoStars;
                newUserRate.ThreeStars = rate.ThreeStars;
                newUserRate.FourStars = rate.FourStars;
                newUserRate.FiveStars = rate.FiveStars;
                _unitOfWork.Rates.Create(newUserRate);
                _unitOfWork.Commit();
                return Accepted("First Rate is Created");
            }
            else 
            {

                userRate.OneStar = rate.OneStar;
                userRate.TwoStars = rate.TwoStars;
                userRate.ThreeStars = rate.ThreeStars;
                userRate.FourStars = rate.FourStars;
                userRate.FiveStars = rate.FiveStars;
                _unitOfWork.Rates.Modifing(userRate);
                _unitOfWork.Commit();
                return Accepted("User Rate Are Modidied Succeefuly");
            }
        }
        [HttpGet("getrate")]
        public IActionResult GetRate(string userId)
        {
            if(userId == null)
            {
                return BadRequest("User Is Worng or Empty");
            }
            var Rate = _unitOfWork.Rates.FindOne(x => x.UserId == userId);
            return Ok(Rate);
        }
    }
}
