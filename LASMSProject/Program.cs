using LASMSProject.Data;
using LASMSProject.Models;
using LeaveSalaryMgmt.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("constr")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//---------------------------------------
// DBSeeder SetUp (to connect with program .cs that it can run first when sytem is run)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;// request services from DI
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Auto-Update Database if you have pending migrations
        context.Database.Migrate();

        // Run Role & Admin Seeder
        await DbSeeder.SeedRolesAndAdminAsync(services);

        // Run Leave Type Seeder
        await DbSeeder.SeedLeaveTypesAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
//------------------------------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
