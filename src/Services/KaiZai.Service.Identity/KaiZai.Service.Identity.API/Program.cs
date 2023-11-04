using IdentityServerHost.Quickstart.UI;
using KaiZai.Service.Identity.API.Quickstart;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentityServer()
        .AddInMemoryApiScopes(IdentityBaseConfig.GetApiScopes())
        .AddInMemoryApiResources(IdentityBaseConfig.GetApiResources())
        .AddInMemoryIdentityResources(IdentityBaseConfig.GetIdentityResources())
        .AddTestUsers(TestUsers.Users)
        .AddInMemoryClients(IdentityBaseConfig.GetClients())
        .AddDeveloperSigningCredential();

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
app.UseIdentityServer();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
