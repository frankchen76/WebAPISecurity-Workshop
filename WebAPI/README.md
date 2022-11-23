## WebAPI

This is a sample to showcase how to secure a ASP.Net Core Web API using AAD

### Instructions:
* In order to run this sample, you need to create appsettings.json under project root
* add the following content to the appsettings.json

```json
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft": "Warning",
            "Microsoft.Hosting.Lifetime": "Information"
        }
    },
    "AzureAd": {
        "Instance": "https://login.microsoftonline.com/",
        "Domain": "[tenant name].onmicrosoft.com",
        "TenantId": "[Tenant Id]",
        "ClientId": "[Client Id]"
    },
    "AllowedHosts": "*"
}
```

### WeatherAPI
This sample walks through the detail protection steps by leveraging JwtBearer token.

### WeatherAPI02
This sample walks through the detail protection steps by leveraging "Microsoft.Identity.Web" nuget package

### CouponAPI
This sample walks through: 
* Leverage ASP.NET Core 5.0 to build a Web API. 
* Secure Web API using "Microsoft.Identity.Web". 
* Leverage Entity Framework Core to access Azure SQL DB using "DefaultCredential" which can use Managed Id to access DB. 
* Access Always Encrypted Column feature fro Azure SQL DB. 

### CouponAPI2
This sample walks through: 
* Leverage ASP.NET Core 6.0 to build a Web API. 
* Secure Web API using "Microsoft.Identity.Web". 
* Leverage Entity Framework Core to access Azure SQL DB using "DefaultCredential" which can use Managed Id to access DB. 
* Access Always Encrypted Column feature for Azure SQL DB using "DefaultCredential" which can use System assigned Managed Id
* Demonstrated the downstream API patterns to call a MS Graph API

### CouponAPI3
This sample walks through: 
* Leverage ASP.NET Core 7.0 to build a Web API. 
* Secure Web API using "Microsoft.Identity.Web". 
* Leverage Entity Framework Core to access Azure SQL DB using "DefaultCredential" which can use Managed Id to access DB. 
* Demonstrated the downstream API patterns to call a MS Graph API
