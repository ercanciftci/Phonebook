using MassTransit;
using Microsoft.Extensions.Options;
using Phonebook.ReportAPI.Consumers;
using Phonebook.ReportAPI.Services;
using Phonebook.ReportAPI.Settings;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateReportMessageCommandConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("create-report-service", e =>
        {
            e.ConfigureConsumer<CreateReportMessageCommandConsumer>(context);
        });
    });
});

builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddHttpClient();

builder.Services.AddControllers();

builder.Services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
