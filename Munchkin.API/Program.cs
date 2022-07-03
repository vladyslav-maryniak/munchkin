using MediatR;
using Microsoft.Extensions.Options;
using Minio;
using Munchkin.API;
using Munchkin.API.Filters;
using Munchkin.Application.DbContext.MongoDb;
using Munchkin.Application.DbContext.MongoDb.Base;
using Munchkin.Application.DbContext.MongoDb.Extensions;
using Munchkin.Application.Hubs;
using Munchkin.Application.Services;
using Munchkin.Application.Services.Base;
using Munchkin.Domain.Behaviours;
using Munchkin.Domain.Validation;
using Munchkin.Shared.Identity;
using Munchkin.Shared.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHostedService<EventHostedService>();
builder.Services.AddSingleton<IEventService, EventStoreService>();
builder.Services.AddSingleton<IGameRepository, GameRepository>();

builder.Services.Configure<MinioOptions>(builder.Configuration.GetSection(nameof(MinioOptions)));
builder.Services.Configure<EventStoreOptions>(builder.Configuration.GetSection(nameof(EventStoreOptions)));

builder.Services.AddTransient<IImageService, ImageService>(provider =>
{
    var minioOptions = provider.GetRequiredService<IOptions<MinioOptions>>();
    var client = new MinioClient()
        .WithEndpoint(minioOptions.Value.Endpoint)
        .WithCredentials(minioOptions.Value.AccessKey, minioOptions.Value.SecretKey)
        .Build();
    return new ImageService(client, minioOptions);
});

builder.Services.AddSignalR();
builder.Services.AddMediatR(typeof(Munchkin.Domain.Entrypoints.MediatREntrypoint).Assembly);
builder.Services.AddAutoMapper(
    assemblies: new[]
    {
        typeof(Munchkin.Domain.Entrypoints.AutoMapperEntrypoint).Assembly,
        typeof(Munchkin.API.Entrypoints.AutoMapperEntrypoint).Assembly
    }
);

builder.Services.AddValidators();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

builder.Services.AddMongoDbContext<IMunchkinDbContext,MunchkinDbContext>(options =>
{
    options.ConnectionString = builder.Configuration["MongoDbOptions:ConnectionString"];
    options.DatabaseName = builder.Configuration["MongoDbOptions:DatabaseName"];
});

var identityMongoDbOptions = builder.Configuration
    .GetSection(nameof(IdentityMongoDbOptions))
    .Get<IdentityMongoDbOptions>();
builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>(
        options =>
        {
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._-";
            options.User.RequireUniqueEmail = true;
        })
    .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
        identityMongoDbOptions.ConnectionString, identityMongoDbOptions.DatabaseName);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});

builder.Services.AddControllers(options => options.Filters.Add(typeof(ResponseMappingFilter)));
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

app.UseCors(builder => builder
    .SetIsOriginAllowed(host => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
);

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<EventHub>("/api/event");

app.Run();
