using eVoucher.ClientAPI_Integration;
using eVoucher_BUS.FrontendServices;
using eVoucher_DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.AccessDeniedPath = "/Login/Forbidden/";
    });
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddTransient<BaseAPIClient>();
builder.Services.AddTransient<GameAPIClient>();
builder.Services.AddTransient<StaffAPIClient>();
builder.Services.AddTransient<LoginAPIClient>();
builder.Services.AddTransient<PartnerAPIClient>();
builder.Services.AddTransient<CampaignAPIClient>();
builder.Services.AddTransient<CustomerAPIClient>();
builder.Services.AddTransient<StatisticAPIClient>();
builder.Services.AddTransient<GoogleDistanceMatrixAPICLient>();
builder.Services.AddTransient<ICommonService, CommonService>();
builder.Services.AddTransient<IFrStaffService, FrStaffService>();
builder.Services.AddTransient<IFrPartnerService, FrPartnerService>();
builder.Services.AddTransient<IFrCampaignService, FrCampaignService>();
builder.Services.AddTransient<IFrCustomerService, FrCustomerService>();
builder.Services.AddTransient<IFrStatisticService, FrStatisticService>();
builder.Services.AddRazorPages();

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
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
