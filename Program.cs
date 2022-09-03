using Cars.Reservation.Api.Extensions.ServiceCollection;
using Cars.Reservation.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMapperProfiles();
builder.Services.AddRepositories();
builder.Services.AddDomainServices();
builder.Services.AddApplicationServices();
builder.Services.AddCustomHealthCheck();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsHelper.CorsPolicyName, policy =>
    {
        policy.WithOrigins(CorsHelper.GetOrigins(configuration["AllowedClients"]))
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors(CorsHelper.CorsPolicyName);
app.UseAuthorization();

app.MapControllers();

app.Run();
