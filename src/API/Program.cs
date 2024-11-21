using Infrastructure.Extensions;
using Infrastructure.Seeders;
using Application.Extensions;
using Serilog;
using API.Middlewares;
using Domain.Entities;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.AddPresentation();


var app = builder.Build();


// Seed the database
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

app.UseSerilogRequestLogging();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapIdentityApi<AppUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program { }