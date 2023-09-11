using Microsoft.EntityFrameworkCore;
using WiredBrainCoffeeAdmin.Data;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<WiredContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("WiredBrain")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddHttpClient<ITicketService, TicketService>(client => client.BaseAddress = new Uri("https://wiredbraincoffeeadmin.azurewebsites.net/"));
builder.Services.AddHttpClient();

var app = builder.Build();

await EnsureDbCreated(app.Services, app.Logger);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

async Task EnsureDbCreated(IServiceProvider services, ILogger logger)
{
    using var db = services.CreateScope().ServiceProvider.GetRequiredService<WiredContext>();
    await db.Database.MigrateAsync();
}
