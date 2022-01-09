using AdressBook.Core.IConfiguration;
using AdressBook.Data;
using AdressBook.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IContactService, ContactService>();
/*var a = builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>();*/
ConfigureDatabase();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

void ConfigureDatabase()
{
    var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
    var connection = new SqliteConnection(connectionStringBuilder.ToString());
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connection));

    var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connection).Options;
    var context = new AppDbContext(options);

    context.Database.OpenConnectionAsync();
    context.Database.EnsureCreatedAsync();
    InMemoryDbHelper.CreateBasicData(context);
}