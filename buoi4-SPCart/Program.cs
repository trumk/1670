﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using buoi4_SPCart.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<buoi4_SPCartContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("buoi4_SPCartContext") ?? throw new InvalidOperationException("Connection string 'buoi4_SPCartContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc();
//set session timeout, default is 20 mins
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromSeconds(30);
});

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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
