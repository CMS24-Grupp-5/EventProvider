using EventProvider.Data;
using EventProvider.Models;
using EventProvider.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEventService, EventService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Events.AddRange(new List<Event>
    {
        new Event { Title = "Musikfestival", Date = DateTime.Parse("2025-06-15"), Location = "Stockholm", Description = "Beskrivning saknas." },
        new Event { Title = "Hundmässan", Date = DateTime.Parse("2025-05-08"), Location = "Göteborg", Description = "Beskrivning saknas." },
        new Event { Title = "Chokladfestival", Date = DateTime.Parse("2025-07-20"), Location = "Malmö", Description = "Beskrivning saknas." }
    });
    db.SaveChanges();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
