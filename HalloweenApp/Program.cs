using HalloweenApp.Infrastrukture.Reactivity;
using HalloweenApp.Infrastrukture.Data;
using HalloweenApp.Application;
using HalloweenApp.Components;
using HalloweenApp.Sample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSignalR(options =>
{ 
    options.MaximumReceiveMessageSize = 1024 * 1024 * 10;
});

builder.Services.AddBlazorBootstrap();

//Add layers
builder.Services
    .AddApplication()
    .AddDataInfrastructure();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.MapBlazorHub();
app.MapHub<ClickerHub>("/clickerHub");
app.MapHub<EventHub>("/eventHub");

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
