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

