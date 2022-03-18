using Accelerator.API.Shared.Mappers;
using Accelerator.API.Shared.Models;
using Accelerator.API.Shared.Services;
using Accelerator.Contracts;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
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
            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<IMapper<QuestionsDTO, Questions>, QuestionMapper>();
        }
    }
}
