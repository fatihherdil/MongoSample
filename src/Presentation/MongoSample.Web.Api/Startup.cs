using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoSample.Application.Extensions;
using MongoSample.Application.Interfaces;
using MongoSample.Application.Middlewares;
using MongoSample.Application.Repositories;
using MongoSample.Application.Resolvers;
using MongoSample.Domain.Entities;
using MongoSample.Infrastructure.Logging.Providers;
using System.IO.Compression;

namespace MongoSample.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region ResponseCompression

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.Configure<BrotliCompressionProviderOptions>(o => o.Level = CompressionLevel.Optimal);
            services.Configure<GzipCompressionProviderOptions>(o => o.Level = CompressionLevel.Optimal);

            #endregion

            services.AddControllers().AddNewtonsoftJson(options=>
            {
                options.SerializerSettings.ContractResolver = new EntityContractResolver();
                options.UseCamelCasing(true);
            });
            services.AddMongoDb(Configuration);
            services.AddTransient<IMongoRepository<User>, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddProvider(new FileLogProvider());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<ExceptionHandlingMiddleware>();
            }

            app.UseResponseCompression();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
