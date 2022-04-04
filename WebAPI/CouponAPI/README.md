# Module06-CouponAPI

This sample demonstrated the custom API built by .NET Core

## Instruction: 
* run below command to install dotnet core tools
  * dotnet-aspnet-codegenerator ASP.NET Code Generator Cli
  * dotnet-ef: Entity Framework Core Cli
```
dotnet tool install -g dotnet-aspnet-codegenerator
dotnet tool install -g dotnet-ef
```
* run below steps to install the dependencies
```
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.Data.SqlClient
```
### For code first approach
* create model and dbcontext

* run the following cmd to generate controller class
```
dotnet aspnet-codegenerator controller -name CouponController -async -api -m Coupon -dc CouponContext -outDir Controllers -f
dotnet aspnet-codegenerator controller -name CouponCodeController -async -api -m CouponCode -dc CouponContext -outDir Controllers -f
```
* Run the following cmd to generate the table to Sql database
```
# create a inital migration
dotnet ef migrations add initial
# create another migration
dotnet ef migrations add 20220330

# run the migration
dotnet ef database update

# check the migration status
dotnet ef migrations list
```
### For database first approach
 * run the following command to generate context and model class. You need to use SQL authentication to run the below command. Alternative approach for hiding password is to leverage secret.json approach from VS

```
dotnet ef dbcontext scaffold "Data Source=m365x725618-contosodb01.database.windows.net;Database=CouponDB; User Id=[azure-sql-userid];Password=[azure-sql-userpassword;" Microsoft.EntityFrameworkCore.SqlServer -o Models --table Coupons --table CouponCodes --context CouponContext --force

```

### Authentication
* Add the following package for authentication
```
dotnet add package Microsoft.Identity.Web
```
* Create appsettings.json
  * Column Encryption Setting=enabled: enable Always encryption for column encryption. 
  * Authentication=Active Directory Default: enable default authenticaiton which will use the below method: 
    * VSCode: Azure connection account
    * Vistual Studio: Azure Subscription account
    * System assigned Managed Id: use default System assigned managed Id
    * User Assigned Managed Id: add "User Id=[AppId]" in your connection string if you need to specify User Assigned Managed Id. 
```JSON
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft": "Warning",
            "Microsoft.Hosting.Lifetime": "Information"
        }
    },
    "ConnectionStrings": {
        "CouponDB": "Data Source=m365x725618-contosodb01.database.windows.net; Column Encryption Setting=enabled; Authentication=Active Directory Default; Initial Catalog=CouponDB;"
    },
    "AzureAd": {
        "Instance": "https://login.microsoftonline.com/",
        "Domain": "[tenantname]", //m365x000000.onmicrosoft.com
        "ClientId": "[aad-app-id]",
        "TenantId": "common" //use common for multi-tenant, or tenant id for single tenant
    },
    "AllowedHosts": "*"
}
```

### Access Encrypted Column
Added the following code to use Managed Id via "DefaultAzureCredential" to access encrypted column "Startup.cs".

```CSharp
public void ConfigureServices(IServiceCollection services)
{
    // Register Azure KeyVault provider
    SqlColumnEncryptionAzureKeyVaultProvider akvProvider = new SqlColumnEncryptionAzureKeyVaultProvider(new DefaultAzureCredential());
    Dictionary<string, SqlColumnEncryptionKeyStoreProvider> providers = new Dictionary<string, SqlColumnEncryptionKeyStoreProvider>();
    providers.Add(SqlColumnEncryptionAzureKeyVaultProvider.ProviderName, akvProvider);
    SqlConnection.RegisterColumnEncryptionKeyStoreProviders(providers);

    // other init code
}
```