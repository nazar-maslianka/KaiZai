using KaiZai.Service.Expenses.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureMongoDatabase(builder.Configuration);
builder.Services.ConfigureMassTransit(builder.Configuration);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddSerilog(logger);
builder.Services.AddCategoriesClientHttpHandler();
builder.Services.AddRepositories();
builder.Services.AddBusinessServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
     app.UseSwaggerUI(s => 
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "KaiZai Expenses Service API v1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
