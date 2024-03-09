using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Net;
using QuanLyVatTuCungUngg.services.implement;
using QuanLyVatTuCungUngg.services;
using QuanLyVatTuCungUngg.util.Imp;
using QuanLyVatTuCungUngg.util;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectStr")));

// builder.Services.AddDbContext<ApplicationDbContextMySQl>(options =>
//     options.builder.Configuration.GetConnectionString("connectStringMySql")));
builder.Services.AddSignalR();

var _authkey = builder.Configuration.GetValue<string>("JwtSettings:securitykey");
//=======TAO COOKIE=========
builder.Services.AddAuthentication(item =>
{ 
    item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(item =>
{
    item.RequireHttpsMetadata = true;
    item.SaveToken = true;
    item.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authkey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew=TimeSpan.Zero
    };
    //ki gui token vao cookie
    item.Events = new JwtBearerEvents{
        OnMessageReceived = context =>{
            var token = context.Request.Cookies["token"];
            context.Token = token;
            return Task.CompletedTask;
        }
    };
});
//=======TAO COOKIE=========
builder.Services.AddHttpContextAccessor();
//=======DEPEDENCY INJECTION=============
builder.Services.AddScoped<IWorkService, WorkService>();
builder.Services.AddScoped<IUserService, UserService>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var _jwtSetting = builder.Configuration.GetSection("JwtSettings");

builder.Services.Configure<JwtSettings>(_jwtSetting);
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

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;
    if(response.StatusCode==404){
        response.Redirect("/404");
    }else if(response.StatusCode==500){
        response.Redirect("/500");
    }
    if (response.StatusCode == (int)HttpStatusCode.Unauthorized || response.StatusCode == (int)HttpStatusCode.Forbidden)
        response.Redirect("/login");
});

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<Chat>("/chathub");
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
