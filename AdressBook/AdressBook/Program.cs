using AdressBook.Core.IConfiguration;
using AdressBook.Data;
using AdressBook.Helpers;
using AdressBook.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IContactService, ContactService>();
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