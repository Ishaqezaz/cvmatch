using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace api.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var claim = user.FindFirst(JwtRegisteredClaimNames.Sub) ??
                        user.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                throw new Exception("User ID claim not found.");

            if (!int.TryParse(claim.Value, out int userId))
                throw new FormatException($"User ID claim is not a valid integer: {claim.Value}");

            return userId;
        }
    }
}
