using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace WeatherAPI.Extensions
{
    public static class WebAPIServiceCollectionExtensions
    {
        public static IServiceCollection AddProtectedWebApi(
            this IServiceCollection services,
            IConfiguration configuration,
            string configSectionName = "AzureAd")
        {
            services.AddAuthentication(AzureADDefaults.JwtBearerAuthenticationScheme)
                    .AddAzureADBearer(options => configuration.Bind("AzureAd", options));

            services.Configure<JwtBearerOptions>(AzureADDefaults.JwtBearerAuthenticationScheme, options =>
            {
                // This is an Azure AD v2.0 Web API
                options.Authority += "/v2.0";
                //options.TokenValidationParameters.
                // The valid audiences are both the Client ID (options.Audience) and api://{ClientID}
                options.TokenValidationParameters.ValidAudiences = new string[] { options.Audience, $"api://{options.Audience}" };

                // Instead of using the default validation (validating against a single tenant, as we do in line of business apps),
                // we inject our own multitenant validation logic (which even accepts both V1 and V2 tokens)
                options.TokenValidationParameters.IssuerValidator = AadIssuerValidator.ValidateAadIssuer;

                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = async context =>
                    {
                        Console.WriteLine($"failed. {context.Exception?.Message}");
                        await Task.CompletedTask;
                    },
                    OnTokenValidated = async context => 
                    {
                        // This check is required to ensure that the Web API only accepts tokens from tenants where it has been consented and provisioned.
                        if (!context.Principal.Claims.Any(x => x.Type == ClaimConstants.Scope)
                         && !context.Principal.Claims.Any(y => y.Type == ClaimConstants.Scp)
                         && !context.Principal.Claims.Any(y => y.Type == ClaimConstants.Roles)
                         && !context.Principal.Claims.Any(y => y.Type == ClaimConstants.Role))
                        {
                            throw new UnauthorizedAccessException("Neither scope or roles claim was found in the bearer token.");
                        }

                        await Task.CompletedTask;
                    }
                };

            });

            return services;
        }
    }
}
