using CityService.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICityService, CityService.Service.Implementations.CityService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var cityService = app.Services.GetService<ICityService>();
cityService.AddCity(new City { Name = "Warsaw", Population = 1800000, Country = "Poland" });
cityService.AddCity(new City { Name = "Berlin", Population = 3500000, Country = "Germany" });
cityService.AddCity(new City { Name = "Paris", Population = 2200000, Country = "France" });

cityService.AddRegion(new Region { Country = "Poland", City = "Europe" });
cityService.AddRegion(new Region { Country = "Germany", City = "Europe" });
cityService.AddRegion(new Region { Country = "France", City = "Europe" });



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
