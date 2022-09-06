using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservations.Api.Models;
using Reservations.Api.Models.Database;
using Reservations.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injection
builder.Services.AddDbContext<ReservationsDbContext>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("hotels")));
builder.Services.AddScoped<IReservationService, ReservationService>();

//Enable CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowOrigin", options =>
    {
        options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger(x => x.SerializeAsV2 = true);

app.UseHttpsRedirection();

//Use CORS
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

//Controllers Mapping
//Get all reservations
app.MapGet("/reservations", ([FromServices] IReservationService service) =>
{
    return service.GetReservations();
});

//Get by id
app.MapGet("/reservations/{id}", (int id, [FromServices] IReservationService service) =>
{
    return service.GetReservationById(id);
});

//Post reservation
app.MapPost("/reservations", (Reservation reservation, [FromServices] IReservationService service) =>
{
    service.AddReservation(reservation);
});

//Put reservation
app.MapPut("/reservations", (Reservation reservation, [FromServices] IReservationService service) =>
{
    service.UpdateReservation(reservation);
});

//Delete reservation
app.MapDelete("/reservations{id}", (int id, [FromServices] IReservationService service) =>
{
    service.DeleteReservation(id);
});

app.Run();
