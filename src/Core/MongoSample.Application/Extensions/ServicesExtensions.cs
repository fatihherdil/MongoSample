using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoSample.Persistence;
using MongoSample.Persistence.Models;
using System;

namespace MongoSample.Application.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, Action<MongoDbOptions> options)
        {
            if (options == null)
                throw new ArgumentNullException($"{nameof(options)} Should be Given for MongoContext to Build");
            services.AddTransient<MongoContext>();
            services.Configure(options);
            return services;
        }
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException($"{nameof(configuration)} Should be Given for MongoContext to Build");

            services.AddTransient<MongoContext>();
            services.Configure<MongoDbOptions>(configuration.GetSection(nameof(MongoDbOptions)));
            return services;
        }
    }
}
