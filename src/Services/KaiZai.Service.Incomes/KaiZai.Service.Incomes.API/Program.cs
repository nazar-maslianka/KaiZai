using GrpcIncomes;
using KaiZai.Service.Incomes.API;
using KaiZai.Service.Incomes.API.Extensions;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls();

builder.Services.ConfigureMongoDatabase(builder.Configuration);
builder.Services.ConfigureMassTransit(builder.Configuration);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
// Add services to the container.
builder.Services.AddSerilog(logger);
builder.Services.AddGrpc();
builder.Services.AddCategoriesClientHttpHandler();
builder.Services.AddRepositories();
builder.Services.AddBusinessServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "V1",
        Title = "Incomes Microservice API",
        Description = "API for managing incomes in the KaiZai",
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "incomes/{documentname}/swagger.json";
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/incomes/v1/swagger.json", "KaiZai Incomes API V1");
        c.RoutePrefix = "incomes";
    });
}

app.MapGrpcService<IncomesGrpcService>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

