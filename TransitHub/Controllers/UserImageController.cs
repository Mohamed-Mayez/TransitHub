using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransitHubRepo;
using TransitHubRepo.Models;

namespace TransitHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserImageController : ControllerBase
    {
        IUnitOfWork _unitOfWork;
        public UserImageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("CreateUserImg")]
         public async Task<IActionResult> CreateImage(IFormFile file,string UserId) 
         {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No File Uploaded :( ");
            }
            // Save Imge To Server 
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine("wwwroot/personalImg", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            //Genrate ImgUrl
            var imgUrl = $"{Request.Scheme}://{Request.Host}/personalImg/{fileName}";
            //save imgUrl in DataBase
            UserImage image = new UserImage();
            image.Name = file.FileName;
            image.Url = imgUrl;
            image.UserId = UserId;
            _unitOfWork.UserImages.Create(image);
            _unitOfWork.Commit();
            return Accepted(image);
         }
        [HttpGet("AllUsersImges")]
         public IActionResult GetAllUsersImges()
         {
            var usersImges = _unitOfWork.UserImages.GetAll();
            return Ok(usersImges);
         }
        [HttpGet("GetImgeById")]
        public IActionResult GetUsersImge(string id)
        {
            var imgeUrl = _unitOfWork.UserImages.FindOne(i => i.UserId == id);
            return Ok(imgeUrl);
        }
        [HttpPost("UpdateUserImage")]
        public async Task<IActionResult> UpdatePersonalImage(IFormFile file, string UserId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No File Uploaded :( ");
            }
            var imageModel = _unitOfWork.UserImages.FindOne(i => i.UserId == UserId);
            // Save Imge To Server 
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine("wwwroot/personalImg", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            //Genrate ImgUrl
            var imgUrl = $"{Request.Scheme}://{Request.Host}/personalImg/{fileName}";
            imageModel.Url = imgUrl;
            _unitOfWork.UserImages.Modifing(imageModel);
            _unitOfWork.Commit();
            return Ok(imageModel);

        }
    }
}
