using Microsoft.AspNetCore.Authentication.JwtBearer;
using SPFxWorkshop.CouponAPI2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider;
using Azure.Identity;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// AddAuthentication() and AddMicrosoftIdentityWebApi secure the web api
// EnableTokenAcquisitionToCallDownstreamApi() and AddInMemoryTokenCaches() allows to call another API.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddDownstreamWebApi("GraphAPI", builder.Configuration.GetSection("GraphAPI"))
    .AddInMemoryTokenCaches();

// Register Azure KeyVault provider
SqlColumnEncryptionAzureKeyVaultProvider akvProvider = new SqlColumnEncryptionAzureKeyVaultProvider(new DefaultAzureCredential());
Dictionary<string, SqlColumnEncryptionKeyStoreProvider> providers = new Dictionary<string, SqlColumnEncryptionKeyStoreProvider>();
providers.Add(SqlColumnEncryptionAzureKeyVaultProvider.ProviderName, akvProvider);
SqlConnection.RegisterColumnEncryptionKeyStoreProviders(providers);

builder.Services.AddDbContext<CouponContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("CouponDB"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//use Cors. NOTE: this has to be within UseRouting() and UseEndpoints();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
