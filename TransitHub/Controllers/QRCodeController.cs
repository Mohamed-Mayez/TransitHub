using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TransitHubEFCore;
using TransitHubRepo;
using TransitHubRepo.Dto;
using TransitHubRepo.Models;

namespace TransitHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public QRCodeController(IUnitOfWork UnitOfWork, UserManager<ApplicationUser> UserManager)
        {
            _UnitOfWork = UnitOfWork;
            _userManager = UserManager;
        }
        [HttpPost("CreateQR")]
       
        public IActionResult CreateQR(QRcodeCreationDto qRcode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            OrderQR qr = new OrderQR();
            qr.QRCode    = qRcode.QRcode;
            qr.Price     = qRcode.Price;
            qr.CarrierId = qRcode.CarrierId;
            qr.SenderId  = qRcode.SenderId;
            qr.Created   = true;
            qr.Scaned    = false;
            _UnitOfWork.QRCode.Create(qr);
            _UnitOfWork.Commit();
            return Accepted("The QR Code Created Succefully ");
        }
        [HttpGet("ScanQR")]
        public async Task<IActionResult> ScanQR(string qrCode, string carriedId)
        {
            var user = await _userManager.FindByIdAsync(carriedId);
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var result = _UnitOfWork.QRCode.ScanQR(qrCode, carriedId);
            var price = _UnitOfWork.QRCode.FindOne(q => q.CarrierId == carriedId && q.QRCode == qrCode);
            if (result)
            {
                user.NumberOfTrips += 1;
                await _userManager.UpdateAsync(user);
                _UnitOfWork.Commit();
                return Ok(price.Price);
            }
            else
            {
                return BadRequest("The Code Is Not Valid ");
            }

        }
        [HttpGet("cheakscan")]
        public IActionResult CheakScan(string QRcode, string senderId)
        {
            var PassngerId = _UnitOfWork.QRCode.FindOne(q => q.SenderId == senderId && q.QRCode == QRcode);
            if (string.IsNullOrEmpty(QRcode) || string.IsNullOrEmpty(senderId))
            {
                return BadRequest("the qrcode or user id is wrong");
            }
            bool result = _UnitOfWork.QRCode.CheackScan(QRcode,senderId);
            if (result)
            {
                return Ok(PassngerId.CarrierId);
            }
            return BadRequest(result);
        }

    }
}
