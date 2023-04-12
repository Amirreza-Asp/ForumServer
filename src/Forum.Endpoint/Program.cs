using Forum.Application.Services;
using Forum.Domain.Entities.Account;
using Forum.Infrastructure;
using Forum.Infrastructure.Services;
using Forum.Persistence;
using Identity.Endpoint.API.Registrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOption>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddMemoryCache();
builder.Host.UseSerilog((ctx, config) => config.ReadFrom.Configuration(ctx.Configuration));


builder.Services
    .AddPersistenceRegistrations(builder.Configuration)
    .AddInfrastructureRegistrations();


builder.Services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders()
                    .AddRoles<AppRole>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = TokenService.GetKey(),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
        //policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


var scoped = app.Services.CreateScope();
var initializer = scoped.ServiceProvider.GetRequiredService<IDbInitializer>();

app.Lifetime.ApplicationStarted.Register(async () =>
{
    await initializer.Execute();
});

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
