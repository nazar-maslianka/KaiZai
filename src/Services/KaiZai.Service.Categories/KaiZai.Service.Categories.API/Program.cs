using KaiZai.Service.Categories.API.Extensions;
using KaiZai.Service.Common.MessageExchangeBaseConfigurator.MassTransit;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

//builder.Logging.AddSerilog(logger);
builder.Services.AddSerilog(logger);
builder.Services.ConfigureMongoDatabase(builder.Configuration);
builder.Services.ConfigureMassTransit(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddFilters();
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
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "KaiZai Categories API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();
