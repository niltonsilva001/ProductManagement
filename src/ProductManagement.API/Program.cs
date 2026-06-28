using ProductManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ProductManagement.API.Consumers;
using ProductManagement.API.HostedServices;
using ProductManagement.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext com PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Host=localhost;Port=5432;Database=product_management_db;Username=postgres;Password=password123;";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Configurar Injeção de Dependência para os Serviços
builder.Services.AddScoped<ProductManagement.Application.Interfaces.IProductService, ProductManagement.Application.Services.ProductService>();
builder.Services.AddScoped<ProductManagement.Domain.Interfaces.IProductRepository, ProductManagement.Infrastructure.Repositories.ProductRepository>();
builder.Services.AddScoped<FluentValidation.IValidator<ProductManagement.Application.DTOs.CreateProductDto>, ProductManagement.Application.Validators.CreateProductValidator>();
builder.Services.AddScoped<FluentValidation.IValidator<ProductManagement.Application.DTOs.UpdateProductDto>, ProductManagement.Application.Validators.UpdateProductValidator>();
builder.Services.AddScoped<IMessagePublisher, ProductManagement.Application.Publishers.RabbitMqPublisher>();
builder.Services.AddSingleton<ProductEventConsumer>();
builder.Services.AddHostedService<RabbitMqConsumerHostedService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/*Adicionar o middleware de tratamento de exceções, isso é executado automaticamente
 e colocado no pipeline de execução do ASP.NET Core, então não é necessário chamar o método UseMiddleware diretamente.*/
app.UseMiddleware<ProductManagement.API.Middleware.ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
