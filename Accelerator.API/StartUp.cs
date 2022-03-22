using Accelerator.API.Shared.Mappers;
using Accelerator.API.Shared.Models;
using Accelerator.API.Shared.Repositories;
using Accelerator.API.Shared.Services;
using Accelerator.Contracts;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(Accelerator.API.StartUp))]
namespace Accelerator.API
{
    public class StartUp : FunctionsStartup
    {
        private ILoggerFactory _loggerFactory;
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder().AddJsonFile("local.settings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables().Build();
            builder.Services.AddLogging();


            #region database registration
            // set up database configuration using ioc
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            //var connectionString = Environment.GetEnvironmentVariable("LOCAL_CONNECTION_STRING");
            builder.Services.AddSingleton<IMongoClient>(c =>
            {
                return new MongoClient(connectionString);
            });

            builder.Services.AddScoped(c =>
                c.GetRequiredService<IMongoClient>().StartSession());
            #endregion

            #region interface registration
            builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();

            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<IMapper<QuestionsDTO, Questions>, QuestionMapper>();
            #endregion
            ConfigureServices(builder);
        }


        private void ConfigureServices(IFunctionsHostBuilder builder)
        {
            _loggerFactory = new LoggerFactory();
            var logger = _loggerFactory.CreateLogger("Startup");
            logger.LogInformation("Initializing and configuring services in Startup...");
        }
    }
}
