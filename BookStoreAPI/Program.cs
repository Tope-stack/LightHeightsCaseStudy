using AutoMapper;
using BookStoreAPI.DataContext;
using BookStoreAPI.Repository.Interfaces;
using BookStoreAPI.Repository.Services;
using BookStoreAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using BookStoreAPI.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IBookStoreRepository, BookStoreService>();
builder.Services.AddScoped<IOrderRepository, OrderService>();
builder.Services.AddScoped<IMessageProducer, RabbitMQProducer>();

var mapperConfig = new MapperConfiguration(map =>
{
    map.AddProfile<MappingProfiles>();

});

builder.Services.AddSingleton(mapperConfig.CreateMapper());


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BookStore API",
        Version = "v1",
        Description = "BookStore API Desc",
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookSrore API V1");
    });
}

app.UseCors(x => x.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod()
           );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
