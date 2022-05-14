using MediatR;
using Munchkin.API;
using Munchkin.Application.Services;
using Munchkin.Application.Services.Base;
using Munchkin.Shared.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHostedService<EventHostedService>();
builder.Services.AddSingleton<IEventService, EventStoreService>();
builder.Services.AddSingleton<IGameRepository, GameRepository>();

builder.Services.AddSignalR();
builder.Services.AddMediatR(typeof(Munchkin.Domain.Entrypoints.MediatREntrypoint).Assembly);
builder.Services.AddAutoMapper(
    assemblies: new[]
    {
        typeof(Munchkin.Domain.Entrypoints.AutoMapperEntrypoint).Assembly,
        typeof(Munchkin.API.Entrypoints.AutoMapperEntrypoint).Assembly
    }
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<EventHub>("/event");

app.Run();
