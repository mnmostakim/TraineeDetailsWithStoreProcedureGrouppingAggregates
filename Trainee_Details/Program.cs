using Microsoft.EntityFrameworkCore;
using Students_Information.HostedServices;
using Trainee_Details.HostedServices;
using Trainee_Details.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TraineeDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("db")));
builder.Services.AddScoped<ApplyMigrationService>();
builder.Services.AddHostedService<DbMigrationHostedService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.Run();

