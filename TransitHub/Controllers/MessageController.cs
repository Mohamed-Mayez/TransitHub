using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransitHubRepo;
using TransitHubRepo.Dto;
using TransitHubRepo.Models;

namespace TransitHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public MessageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetMessages(string snderId,string receverId)
        {
            if(string.IsNullOrEmpty(snderId) && string.IsNullOrEmpty(receverId))
            {
                return BadRequest("Chek the paramters");
            }
            var messages = _unitOfWork.Messages.FindAll(s => s.Sender == snderId && s.Recever == receverId);
            if(!messages.Any())
            {
                return NoContent();
            }
            return Ok(messages);
        }
        [HttpPost("CreateMessage")]
        public IActionResult SendMessage(SendMessageDto message)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Message mess = new Message();
            mess.Sender = message.SenderId;
            mess.Recever = message.ReseverId;
            mess.MessageContent = message.MessageContent;
            mess.Time = DateTime.Now;
            mess.IsDeleted = false;
            mess.IsRead = false;
            _unitOfWork.Messages.Create(mess);
            _unitOfWork.Commit();
            return Ok();
        }
    }
}
