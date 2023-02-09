using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WPSUR.Services.Constants
{
    public static class AuthenticationOptions
    {
        public const string ISSUER = "InternshipAuthenticationProject";
        public const string AUDIENCE = "Everyone";
        const int EXPIRATION_IN_MINUTES = 60;
        const string KEY = "Zq3t6w9z$C&F)J@NcRfUjXn2r5u7x!A%D*G-KaPdSgVkYp3s6v9y/B?E(H+MbQeThWmZq4t7w!z%C&F)J@NcRfUjXn2r5u8x/A?D(G-KaPdSgVkYp3s6v9y$B&E)H@Mb";

        public static DateTime GetExpirationDate()
            => DateTime.UtcNow.Add(TimeSpan.FromMinutes(EXPIRATION_IN_MINUTES));

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
            => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
