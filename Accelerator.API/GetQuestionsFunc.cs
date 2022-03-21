using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Accelerator.API.Shared.Models;
using Accelerator.API.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Accelerator.API
{
    public class GetQuestionsFunc
    {
        #region constants
        private IQuestionService _questionService;
        #endregion
        public GetQuestionsFunc(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [FunctionName("GetQuestions")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "GetQuestions" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        //[OpenApiParameter(name: "name", In = ParameterLocation.Path, Required = false, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(QuestionGetResponse), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "questions")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetAllQuestion function is triggered...");

            log.LogInformation("Calling Questions service...");
            var response = await _questionService.GetQuestions();

            if(response.Error != null)
            {
                log.LogError($"Getting question returned error {response.Error.Message}...");
                return new BadRequestObjectResult(response);
            }
            log.LogInformation("All Quesions are retrieved succesfully...");
            return new OkObjectResult(response);
        }
    }
}

