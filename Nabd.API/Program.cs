using Nabd.API.Extensions; 
using Nabd.Infrastructure.Extensions; 
using Nabd.Shared.Extensions; 
using Nabd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ====================================================
// 1.  (Services Container)
// ====================================================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddApplicationServices(builder.Configuration);


builder.Services.AddIdentityServices();


builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddCorsConfiguration(builder.Configuration);


var app = builder.Build();

// ====================================================
// 2.  (HTTP Pipeline)
// ====================================================


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {

        var context = services.GetRequiredService<NabdDbContext>();
        await context.Database.MigrateAsync();


        await DatabaseSeederExtension.SeedDatabaseAsync(app);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during migration/seeding.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors("NabdCorsPolicy");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();