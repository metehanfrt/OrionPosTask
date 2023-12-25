using Microsoft.EntityFrameworkCore;
using OrionPosTask.Models.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

// Session servisini akler
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Metehan.Sesion";
    options.IdleTimeout = TimeSpan.FromMinutes(15); // oturumun açýk kalma süresi vs vs
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); //doðrudan appsettings kýsýmýndanda kullanabiliriz
    options.UseSqlServer(connectionString); //constractor 
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    db.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// GDPR Cookie Constent kullanmayý saðlar
app.UseCookiePolicy();

// Session kullanýmýný saðlar
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
