using CertificateGenerator.Worker;
using MassTransit;
using MongoDB.Driver;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    return new MongoClient("mongodb://localhost:27017");
});
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("CertificateManagementDb");
});


builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.AddConsumer<CertificateGenerationConsumer>();

    busConfigurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("certificate-generation-queue", e =>
        {
            e.ConfigureConsumer<CertificateGenerationConsumer>(context);
        });
    });
});

var host = builder.Build();
host.Run();