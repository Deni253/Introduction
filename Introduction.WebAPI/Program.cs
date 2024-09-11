using Autofac;
using Autofac.Extensions.DependencyInjection;
using Introduction.Repository;
using Introduction.Repository.Common;
using Introduction.Service;
using Introduction.Service.Common;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<DogRepository>().As<IDogRepository>().SingleInstance();
    containerBuilder.RegisterType<DogOwnerRepository>().As<IDogOwnerRepository>().SingleInstance();
    containerBuilder.RegisterType<DogService>().As<IDogService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<DogOwnerService>().As<IDogOwnerService>().InstancePerLifetimeScope();
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

app.UseAuthorization();

app.MapControllers();

app.Run();