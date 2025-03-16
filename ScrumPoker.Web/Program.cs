using ScrumPoker.Web.Hubs;
using ScrumPoker.Business;
using ScrumPoker.DataAccess;
using ScrumPoker.DataProvider;
using NLog.Web;
using Microsoft.IdentityModel.Tokens;
using ScrumPoker.Dto;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ScrumPoker.Web.Middleware;
using ScrumPoker.Localization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnectionString"];
NLog.LogManager.Configuration.Variables["dbConnectionString"] = connectionString;

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddLocalizationServices();

builder.Services.Configure<AuthSettingsDto>(builder.Configuration.GetSection("AuthSettings"));
builder.Services.Configure<EmailSettingsModelDto>(builder.Configuration.GetSection("EmailSettings"));

// Add services to the container.
builder.Services.AddBusinessServices();
builder.Services.AddDataAccessServices();
builder.Services.AddDataProviderServices();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllersWithViews(options =>
{
    //options.Filters.Add<JwtAuthorizationFilter>();
}).AddRazorRuntimeCompilation();

builder.Services.AddSignalR();
builder.Services.AddHealthChecks();
builder.Services.AddSession(option =>
{
    //Süre 1 dk olarak belirlendi
    option.IdleTimeout = TimeSpan.FromMinutes(30);
});

// Authentication ve JWT konfig�rasyonu
var authSettings = builder.Configuration.GetSection("AuthSettings").Get<AuthSettingsDto>();
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Key));
builder.Services.AddTransient<JwtAuthorizationFilter>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = authSettings.Issuer,
            ValidAudience = authSettings.Audience,
            IssuerSigningKey = key
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
     
}

app.UseMiddleware<UserTokenDataCookieMiddleware>();
app.UseSession();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllerRoute(name: "default", pattern: "dashboard");

app.MapHub<PokerRoomHub>("/roomHub");

app.MapHealthChecks("/health");

app.Run();
