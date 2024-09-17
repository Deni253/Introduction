using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Introduction.Repository;
using Introduction.Repository.Common;
using Introduction.Service;
using Introduction.Service.Common;
using Introduction.WebAPI.Controllers;
using Introduction.WebAPI.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy(IdentityData.AdminUserPolicyName, p =>
p.RequireClaim(IdentityData.AdminUserClaimName, "true"));

});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<DogRepository>().As<IDogRepository>().SingleInstance();
    containerBuilder.RegisterType<DogOwnerRepository>().As<IDogOwnerRepository>().SingleInstance();
    containerBuilder.RegisterType<DogService>().As<IDogService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<DogOwnerService>().As<IDogOwnerService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<AuthRepository>().As<IAuthRepository>().InstancePerLifetimeScope();

});

// Add services to the container.

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();