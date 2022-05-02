using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using SPFxWorkshop.CouponAPI2.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPFxWorkshop.CouponAPI2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MeController : Controller
    {
        public MeController(ITokenAcquisition tokenAcquisition)
        {
            _tokenAcquisition = tokenAcquisition;
        }

        readonly ITokenAcquisition _tokenAcquisition;

        // GET: api/<controller>
        [HttpGet] 
        public async Task<ActionResult<dynamic>> Get()
        {
            dynamic ret = null;
            //AuthenticationResult result = null;
            try
            {
                ret = await CallGraphApiOnBehalfOfUser();//.GetAwaiter().GetResult();
                //TodoStore.Add(new TodoItem { Owner = owner, Title = title });
            }
            catch (MsalException ex)
            {
                HttpContext.Response.ContentType = "text/plain";
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await HttpContext.Response.WriteAsync("An authentication error occurred while acquiring a token for downstream API\n" + ex.ErrorCode + "\n" + ex.Message);
            }
            catch (Exception ex)
            {
                HttpContext.Response.ContentType = "text/plain";
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await HttpContext.Response.WriteAsync("An error occurred while calling the downstream API\n" + ex.Message);
            }
            return Ok(ret);
            //return new WeatherForecast[] { new WeatherForecast() { User = ownerName } };

        }
        public async Task<UserInfo> CallGraphApiOnBehalfOfUser()
        {
            string[] scopes = { "user.read" };
            UserInfo ret = null;

            // we use MSAL.NET to get a token to call the API On Behalf Of the current user
            try
            {
                string accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(scopes);
                ret = await CallGraphApiOnBehalfOfUser(accessToken);
            }
            catch (MsalUiRequiredException ex)
            {
                _tokenAcquisition.ReplyForbiddenWithWwwAuthenticateHeader(scopes, ex);
            }
            return ret;
        }

        private static async Task<UserInfo> CallGraphApiOnBehalfOfUser(string accessToken)
        {
            // Call the Graph API and retrieve the user's profile.
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.GetAsync("https://graph.microsoft.com/v1.0/me");
            string content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                dynamic me = JsonConvert.DeserializeObject(content);
                return new UserInfo()
                {
                    DisplayName=me.displayName,
                    GivenName =me.givenName,
                    Surname=me.surname,
                    JobTitle=me.jobTitle,
                    Mail=me.mail,
                    UserPrincipalName=me.userPrincipalName
                };
            }

            throw new Exception(content);
        }
    }
}
