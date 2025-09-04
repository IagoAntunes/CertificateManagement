using CertificateManagement.Domain.Entities;
using CertificateManagement.Domain.Repository;
using CertificateManagement.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CertificateManagement.Infrastructure
{
    public static class InfraRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,  
            IConfiguration configuration

        )
        {

            services.AddScoped<ICertificateRepository, CertificateRepository>();

            //MongoDb
            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = configuration.GetSection("MongoDbSettings");
                var connectionString = settings["ConnectionString"];
                return new MongoClient(connectionString);
            });

            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var databaseName = configuration["MongoDbSettings:DatabaseName"];
                var database = client.GetDatabase(databaseName);
                var certificateCollection = database.GetCollection<CertificateEntity>("certificates");
                var indexKeysDefinition = Builders<CertificateEntity>.IndexKeys.Ascending(x => x.CreatedAt);
                var indexOptions = new CreateIndexOptions { ExpireAfter = TimeSpan.FromDays(30) };
                var indexModel = new CreateIndexModel<CertificateEntity>(indexKeysDefinition, indexOptions);
                certificateCollection.Indexes.CreateOne(indexModel);
                return database;
            });

            return services;
        }
    }

}
