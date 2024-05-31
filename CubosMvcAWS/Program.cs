using Amazon.S3;
using CubosMvcAWS.Helpers;
using CubosMvcAWS.Models;
using CubosMvcAWS.Services;
using Microsoft.Build.Framework;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string jsonSecrets = await
    HelperSecretManager.GetSecretsAsync();
KeysModel keysModel =
    JsonConvert.DeserializeObject<KeysModel>(jsonSecrets);
builder.Services.AddSingleton<KeysModel>(x => keysModel);

builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddTransient<ServiceCubos>();
builder.Services.AddTransient<ServiceStorageAWS>();
builder.Services.AddControllersWithViews();

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
