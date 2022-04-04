using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPFxWorkshop.CouponAPI.Auth
{
    public class CustomAzureSQLAuthProvider : SqlAuthenticationProvider
    {
        private static readonly string[] _azureSqlScopes = new[] { "https://database.windows.net//.default" };

        // Need to add below code for user assigned managed Id
        //private static readonly TokenCredential _credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions() { ManagedIdentityClientId= "57bcc911-79b1-42c2-8544-76f5cfa5ec83" });
        private static readonly TokenCredential _credential = new DefaultAzureCredential();

        public override async Task<SqlAuthenticationToken> AcquireTokenAsync(SqlAuthenticationParameters parameters)
        {
            var tokenRequestContext = new TokenRequestContext(_azureSqlScopes);
            var tokenResult = await _credential.GetTokenAsync(tokenRequestContext, default);
            return new SqlAuthenticationToken(tokenResult.Token, tokenResult.ExpiresOn);
        }

        public override bool IsSupported(SqlAuthenticationMethod authenticationMethod) => authenticationMethod.Equals(SqlAuthenticationMethod.ActiveDirectoryIntegrated);
    }
}
