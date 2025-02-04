using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace TransitHub.Validatons
{
    public class CustomUserManager<TUser> : UserManager<TUser> where TUser : class
    {
        public CustomUserManager(IUserStore<TUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<TUser> passwordHasher, IEnumerable<IUserValidator<TUser>> userValidators, IEnumerable<IPasswordValidator<TUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<TUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public override async Task<IdentityResult> CreateAsync(TUser user)
        {
            // Custom validation for username
            var userName = await GetUserNameAsync(user);

            // Regular expression pattern to allow Arabic and English letters, digits, and spaces
            var regexPattern = @"^[\p{IsBasicLatin}\p{IsArabic}0-9\s]+$";

            if (string.IsNullOrEmpty(userName) || !Regex.IsMatch(userName, regexPattern))
            {
                return IdentityResult.Failed(new IdentityError { Code = "InvalidUserName", Description = "Username must contain Arabic or English letters, digits, and spaces only." });
            }

            // Call base implementation of CreateAsync if validation passes
            return await base.CreateAsync(user);
        }
    }


}
