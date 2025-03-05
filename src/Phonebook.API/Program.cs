using Microsoft.EntityFrameworkCore;
using Phonebook.API.Helpers;
using Phonebook.Application.Interfaces;
using Phonebook.Application.Services;
using Phonebook.Infrastructure.Persistence.EFConfig;
using Phonebook.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    // options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));
    options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=Password123;Database=phonebookDb");
});

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();

var app = builder.Build();


// Apply Migrations that hasn't been applied yet
using (var scope = app.Services.CreateScope()) // Creates a new scope, ensuring that services resolved within it are properly disposed of when done.
{
    var services = scope.ServiceProvider;
    try
    {
        var appDbContext = services.GetRequiredService<AppDbContext>();
        await appDbContext.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}


// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
