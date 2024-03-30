using Microservice.Gateway;
using Microservice.Gateway.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using Ocelot.Values;
using Polly;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppAuthetication();
if (builder.Environment.EnvironmentName.ToString().ToLower().Equals("production"))
{
    builder.Configuration.AddJsonFile("ocelot.Production.json", optional: false, reloadOnChange: true);
}
else
{
    builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
}
builder.Services.AddHttpClient();   
//builder.Services.AddHttpClient("Mango.Services.API", c =>
//{
//    c.BaseAddress = new Uri("https://localhost:44366");
//}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(30)))
//  .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(2, TimeSpan.FromSeconds(30)));

builder.Services.AddOcelot()
    .AddPolly(); // Add Polly support to Ocelot

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseOcelot().GetAwaiter().GetResult();

app.Run();
