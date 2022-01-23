using AdressBook.Core.IConfiguration;
using AdressBook.Data;
using AdressBook.Helpers;
using AdressBook.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var logPath = Path.Combine(Directory.GetCurrentDirectory(),"Logs");
NLog.GlobalDiagnosticsContext.Set("LogDirectory", logPath);

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(opt =>
{
    opt.ClearProviders();
    opt.SetMinimumLevel(LogLevel.Trace);
    // Trace, Debug, Info, Warning, Error, Fatal
}).UseNLog();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IExportService, XLSXService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
InMemoryDbHelper.ConfigureDatabase(builder);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();