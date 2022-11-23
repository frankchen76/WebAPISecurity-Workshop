using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using SPFxWorkshop.CouponAPI3.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// AddAuthentication() and AddMicrosoftIdentityWebApi secure the web api
// EnableTokenAcquisitionToCallDownstreamApi() and AddInMemoryTokenCaches() allows to call another API.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddDownstreamWebApi("GraphAPI", builder.Configuration.GetSection("GraphAPI"))
    .AddInMemoryTokenCaches();

builder.Services.AddDbContext<CouponContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("CouponDB"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
