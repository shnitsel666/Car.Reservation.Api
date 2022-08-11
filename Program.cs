using Car.Reservation.Api.Extensions.ServiceCollection;
using Car.Reservation.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMapperProfiles();
builder.Services.AddRepositories();
builder.Services.AddServices();
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
