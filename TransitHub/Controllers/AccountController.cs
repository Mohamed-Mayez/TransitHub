using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TransitHubRepo;
using TransitHubRepo.Dto;
using TransitHubRepo.Models;

namespace TransitHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _config = config;
            _unitOfWork = unitOfWork;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Registration( [FromForm]RegisterUserDto userDto,IFormFile file) 
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var ValidEmailUser = await _userManager.FindByEmailAsync(userDto.Email);
            if(ValidEmailUser != null)
            {
                return BadRequest("Email Is Aready Taken, Chose another Email");
            }
            
            
            ApplicationUser user = new ApplicationUser();
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.CardNumberId = userDto.userCardId;
            user.DateOfBirth = userDto.DateOfBirth;
            user.CurrentPassword = userDto.Password;
            
            
           
            IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No File Uploaded :( ");
                }
                // Save Imge To Server 
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine("wwwroot/imgId", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                //Genrate ImgUrl
                var imgUrl = $"{Request.Scheme}://{Request.Host}/imgId/{fileName}";
                user.ImgUrl = imgUrl;
                await _userManager.UpdateAsync(user);
                return Accepted("User Created Successfully",user);
            }
            else
            {
                return BadRequest(result);
            }

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userDto.UserEmail);
                if (user != null)
                {
                    var found =  await _userManager.CheckPasswordAsync(user, userDto.Password);
                    if (found)
                    {
                        // user climes
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name,user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Sub,user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));
                        // user roles
                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                        //create  signingCredentials
                        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:key"]));
                        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                       //creatr Token
                       JwtSecurityToken jwtToken = new JwtSecurityToken(
                            issuer: _config["JWT:issuer"],
                            audience: _config["JWT:audiance"],
                            claims: claims,
                            expires: DateTime.Now.AddDays(10),
                            signingCredentials: credentials

                        );
                        return Ok(
                            new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                                expiration = jwtToken.ValidTo,
                                User = user

                            });
                    }
                }
                return Unauthorized();
            }
            return Unauthorized();
        }
        [HttpPost("UpdatePassword")]
        public async Task<IActionResult> ChangePassword(UpdateUserPasswordDto model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("the filds are empty or null");
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("user is not found");
            }
            var result = await _userManager.ChangePasswordAsync(user, user.CurrentPassword,model.Password);
            if (result.Succeeded)
            {
                user.CurrentPassword = model.Password;
                await _userManager.UpdateAsync(user);
                return Ok(result);
            }
            else 
            {
                return BadRequest();
            }
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("ID is null or empty");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            
            var allUserImages = _unitOfWork.UserImages.FindAll(m => m.UserId == id);
            if (allUserImages.Any() || allUserImages != null)
            {
                _unitOfWork.UserImages.DeleteMany(allUserImages);
            }
            var allCodes = _unitOfWork.QRCode.FindAll(c => c.CarrierId == id || c.SenderId == id);
            if (allCodes.Any() || allCodes != null)
            {
                _unitOfWork.QRCode.DeleteMany(allCodes);
            }
            var allTrips = _unitOfWork.Trips.FindAll(c => c.userId == id);
            if (allTrips.Any() || allTrips != null) 
            {
                _unitOfWork.Trips.DeleteMany(allTrips);
            }
            var allrate = _unitOfWork.Rates.FindAll(c => c.UserId == id);
            if (allrate.Any() || allrate != null)
            {
                _unitOfWork.Rates.DeleteMany(allrate);
            }
            var allLocal = _unitOfWork.Transports.FindAll(c => c.UserId == id);
            if(allLocal.Any() || allLocal != null)
            {
                _unitOfWork.Transports.DeleteMany(allLocal);
            }
            var allImage = _unitOfWork.UserImages.FindAll(c => c.UserId == id);
            if (allImage.Any() || allImage != null)
            {
                _unitOfWork.UserImages.DeleteMany(allImage);
            }
            var allCode = _unitOfWork.QRCode.FindAll(c => c.CarrierId == id || c.SenderId == id);
            if (allCode.Any() || allCode != null)
            {
                _unitOfWork.QRCode.DeleteMany(allCode);
            }
            var allmessage = _unitOfWork.Messages.FindAll(c => c.Sender == id || c.Recever == id);
            if (allmessage.Any() || allmessage != null)
            {
                _unitOfWork.Messages.DeleteMany(allmessage);
            }

            _unitOfWork.Commit();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok("The user was deleted successfully");
            }

            // Collect errors to return detailed information
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return BadRequest($"Failed to delete user: {errors}");
        }
        [HttpGet("getUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                BadRequest("User Not Found");
            }
            return Ok(user);
        }


    }

}

