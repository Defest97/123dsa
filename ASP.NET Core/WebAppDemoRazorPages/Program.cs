using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppDemoRazorPages.Data;
using UAParser;
using NuGet.Protocol.Core.Types;
using System.Text;
using System.Text.Json;
using WebAppDemoRazorPages.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.Configure<AppConfiguration>(builder.Configuration.GetSection("AppConfiguration"));

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.Use(async (context, next) =>
{
    await next.Invoke();
    var configuration = context.RequestServices.GetRequiredService<IOptions<AppConfiguration>>().Value;
    var uaParser = Parser.GetDefault();
    ClientInfo c = uaParser.Parse(context.Request.Headers["User-Agent"]);
    var log = new LoggerIndex();
    log.OS = c.OS.Family;
    log.Path = context.Request.Path;
    log.Browser = c.UA.Family;
    log.Ip = context.Connection.RemoteIpAddress.ToString();
    log.Time = DateTime.Now;
    List<LoggerIndex> logs;
    try
    {
        logs = JsonSerializer.Deserialize<List<LoggerIndex>>(File.ReadAllText(configuration.LogPath));
    }
    catch
    {
        logs = new List<LoggerIndex>();
    }
    logs?.Add(log);
    File.WriteAllText(configuration.LogPath, JsonSerializer.Serialize(logs));


});
app.Run();
