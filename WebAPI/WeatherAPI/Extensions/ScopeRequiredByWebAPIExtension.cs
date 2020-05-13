using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using WeatherAPI.Filters;

namespace WeatherAPI.Extensions
{
    public static class ScopesRequiredByWebAPIExtension
    {
        /// <summary>
        /// When applied to an <see cref="HttpContext"/>, verifies that the user authenticated in the Web API has any of the
        /// accepted scopes. If the authentication user does not have any of these <paramref name="acceptedScopes"/>, the
        /// method throws an HTTP Unauthorized with the message telling which scopes are expected in the token
        /// </summary>
        /// <param name="acceptedScopes">Scopes accepted by this API</param>
        /// <exception cref="HttpRequestException"/> with a <see cref="HttpResponse.StatusCode"/> set to 
        /// <see cref="HttpStatusCode.Unauthorized"/>
        public static void VerifyUserHasAnyAcceptedScope(
            this HttpContext context, 
            params string[] acceptedScopes)
        {
            if (acceptedScopes == null)
            {
                throw new ArgumentNullException(nameof(acceptedScopes));
            }
            Claim scopeClaim = context?.User?.FindFirst(ClaimConstants.Scope);
            //Claim roleClaim = context?.User?.FindFirst(ClaimConstants.Role); 
            if (scopeClaim == null || !scopeClaim.Value.Split(' ').Intersect(acceptedScopes).Any())
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                string message 
                    = $"The 'scope' claim does not contain scopes " +
                    $"'{string.Join(",", acceptedScopes)}' or was not found";
                throw new HttpResponseException()
                {
                    Status = HttpStatusCode.Unauthorized,
                    Value = message
                };
            }
        }
        public static void ValidateAppRole(
            this HttpContext context,
            params string[] appRole)
        {
            //
            // The `role` claim tells you what permissions the client application has in the service.
            // In this case, we look for a `role` value of `access_as_application`.
            //
            //Claim roleClaim = ClaimsPrincipal.Current.FindFirst("roles");
            Claim roleClaim = context?.User?.FindFirst(ClaimConstants.Role);
            //if (roleClaim == null || !roleClaim.Value.Split(' ').Contains(appRole))
            if (roleClaim == null || !roleClaim.Value.Split(' ').Intersect(appRole).Any())
            {
                throw new HttpResponseException()
                {
                    Status = HttpStatusCode.Unauthorized,
                    Value = $"The 'roles' claim does not contain '{appRole}' or was not found"
                };
            }
        }
    }
}
