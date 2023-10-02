using api;
using Microsoft.EntityFrameworkCore;
using Weather.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

string dbConnString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WeatherContext>(options =>
{
    options.UseSqlServer(dbConnString);
});

builder.Services.AddSwaggerGen();

var corsSettings = builder.Configuration.GetSection("CorsSettings").Get<CorsSettingsOption>();
var allowedOrigins = corsSettings.AllowedOrigins;
builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder
        .WithOrigins(allowedOrigins)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithExposedHeaders("X-File-Name");
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
