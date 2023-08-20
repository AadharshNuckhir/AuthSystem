using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Data;
using AuthSystem.Areas.Identity.Data;
using AuthSystem.Chat;
using Microsoft.AspNetCore.SignalR;
using AuthSystem.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthSystemContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthSystemContextConnection' not found.");

builder.Services.AddDbContext<AutSystemContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<AuthSystemUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AutSystemContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddSignalR();

builder.Services.AddScoped<SignalRService>();

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
app.UseAuthentication();;
app.UseAuthorization();

//app.MapPost("broadcast", async (string message, IHubContext<ChatHub, IChatClient> context) =>
//{
//    await context.Clients.All.ReceiveMessage(message);

//    return Results.NoContent;
//});

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<TeamRetroHub>("/teamretrohub"); // Map your SignalR hub
    endpoints.MapHub<CollaborationHub>("/collaborationhub");
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Author", "Agent", "Publisher" };

    foreach (var role in roles)
    {
        if(!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

}

app.Run();
