using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DotNetCoreSqlDb.Data;
var builder = WebApplication.CreateBuilder(args);

// Add database context and cache
builder.Services.AddDbContext<MyDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Data Source=msdocs-core-sql-gte-server.database.windows.net,1433;Initial Catalog=msdocs-core-sql-gte-database;User ID=msdocs-core-sql-gte-server-admin;Password=6H8R4051OS21P650$")));
//builder.Services.AddDistributedMemoryCache();
builder.Services.AddStackExchangeRedisCache(options =>
{
options.Configuration = builder.Configuration["msdocs-core-sql-gte-cache.redis.cache.windows.net:6380,password=Q2xv3eDcGpFa4YcA0fR7qapSIdQUvLSIQAzCaNy379k=,ssl=True,defaultDatabase=0"];
options.InstanceName = "SampleInstance";
});
// Add services to the container.
builder.Services.AddControllersWithViews();

// Add App Service logging
builder.Logging.AddAzureWebAppDiagnostics();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todos}/{action=Index}/{id?}");

app.Run();
