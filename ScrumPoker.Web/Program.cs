using ScrumPoker.Web.Hubs;
using ScrumPoker.Business;
using ScrumPoker.DataAccess;
using ScrumPoker.DataProvider;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBusinessServices();
builder.Services.AddDataAccessServices();
builder.Services.AddDataProviderServices();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
     
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();

app.MapControllerRoute(name: "default", pattern: "dashboard");

app.MapHub<PokerRoomHub>("/roomHub");

app.Run();
