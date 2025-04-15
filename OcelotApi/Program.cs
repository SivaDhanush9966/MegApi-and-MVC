using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add Ocelot with caching and rate limiting
builder.Services.AddOcelot()
    .AddCacheManager(settings => settings.WithDictionaryHandle()); // In-memory cache

// Add Rate Limiting separately 
builder.Services.AddRateLimiting(); 

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.UseOcelot(); // Use Ocelot middleware

app.Run();
