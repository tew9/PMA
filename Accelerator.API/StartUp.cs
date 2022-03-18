using Accelerator.API.Shared.Mappers;
using Accelerator.API.Shared.Models;
using Accelerator.API.Shared.Repositories;
using Accelerator.API.Shared.Repositories.Base;
using Accelerator.API.Shared.Services;
using Accelerator.Contracts;
using Accelerator.Contracts.Base;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Accelerator.API.StartUp))]
namespace Accelerator.API
{
    public class StartUp : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            // set up database configuration using ioc
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            //var connectionString = Environment.GetEnvironmentVariable("LOCAL_CONNECTION_STRING");
            builder.Services.AddSingleton<IMongoClient>(c =>
            {
                return new MongoClient(connectionString);
            });

            builder.Services.AddScoped(c =>
                c.GetRequiredService<IMongoClient>().StartSession());


            builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();

            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<IMapper<QuestionsDTO, Questions>, QuestionMapper>();
        }
    }
}
