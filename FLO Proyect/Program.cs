using FLO_Proyect.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppdbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

builder.Services.AddIdentity<ProgramUser, IdentityRole>()
    .AddEntityFrameworkStores<AppdbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true; //Rəqəm tələb edin
    options.Password.RequireLowercase = false; //Kiçik hərf tələb edin
    options.Password.RequireUppercase = true; //Böyük hərf tələb edin
    options.Password.RequiredLength = 6; //Tələb olunan uzunluq...
    options.Password.RequireNonAlphanumeric = false; //@ * ! ve.s kimi simvollar olmalidi
    options.Lockout.MaxFailedAccessAttempts = 5; //5 girişten sonra bloklanir 
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(5); //bloklamndiqdan 5deq sonra acilir
    options.Lockout.AllowedForNewUsers = true; //yeni qeydiyyat userdirse passwordu unuda biler.bir nece yazdiqda bloklamaya bilersiz
                                               //   options.User.AllowedUserNameCharacters =
                                               //"abcdefghijklmnopqrstuvwxyz0123456789._";//olmasını istediyiniz vacib karaterleri yazin
    options.User.RequireUniqueEmail = true; //unique emaail adresleri olsun (1emaille bir qeydiyyat)
    options.SignIn.RequireConfirmedEmail = false; //qeydiyyat etdikden sonra email ile token gönderecek 
    options.SignIn.RequireConfirmedPhoneNumber = false; //telefon doğrulaması
});

builder.Services.AddScoped<FLO_Proyect.Extensions.IEmailService, FLO_Proyect.Extensions.EmailSender>();

var app = builder.Build();

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

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");




app.Run();

