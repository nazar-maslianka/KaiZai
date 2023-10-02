using KaiZai.Services.Incomes.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureMongoDatabase(builder.Configuration);
builder.Services.ConfigureMassTransit(builder.Configuration);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
// Add services to the container.
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
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

