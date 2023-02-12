using Forum.Application.Services;
using Forum.Infrastructure;
using Forum.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddPersistenceRegistrations(builder.Configuration)
    .AddInfrastructureRegistrations();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scoped = app.Services.CreateScope();
var initializer = scoped.ServiceProvider.GetRequiredService<IDbInitializer>();

app.Lifetime.ApplicationStarted.Register(async () =>
{
    await initializer.Execute();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
