using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace TransitHub.Validatons
{
    public class CustomUserValidator<TUser> : IUserValidator<TUser> where TUser : class
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
           var errors = new List<IdentityError>();
           //Get The userName form UserObject
           var userName = await manager.GetUserNameAsync(user);
            // regular expression pattern to allow arbic and spasces
            var regexPattern = @"^[\p{IsArabic}\s]+$";
            //Check if the userName matches the pattern 
            if (!Regex.IsMatch(userName, regexPattern))
            {
                errors.Add(new IdentityError { Code = "InValidUserName", Description = "Username must contain Arbic characters and spaces only" });
            }
            // if validation succeeds
            return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }
    }
}
