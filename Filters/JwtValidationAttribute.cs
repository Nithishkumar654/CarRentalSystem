using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using CarRental_System.Services;

namespace CarRental_System.Filters
{
    public class JwtValidationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers["Token"].FirstOrDefault();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = authorizationHeader.StartsWith("Bearer ") ? authorizationHeader.Substring(7) : authorizationHeader;

            Console.WriteLine(token);
            var jwtService = context.HttpContext.RequestServices.GetRequiredService<JwtService>();

            if (string.IsNullOrEmpty(token) || !jwtService.ValidateToken(token))
            {
                // If the token is invalid, return Unauthorized
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
