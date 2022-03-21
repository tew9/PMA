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
    public class DeleteQuestionFunc
    {
        #region constants
        private IQuestionService _questionService;
        #endregion
        public DeleteQuestionFunc(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [FunctionName("DeleteQuestion")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "DeleteQuestion" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "QId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **QID** parameter, Three letters Question ID")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(QuestionGetResponse), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "questions/{QId}")] HttpRequest req, string QId,
            ILogger log)
        {
            log.LogInformation("DeleteQuestion function is triggered...");

            if(string.IsNullOrEmpty(QId))
            {
                log.LogError($"Please provide QId parameter...");
                return new BadRequestResult();
            }

            log.LogInformation("Calling Questions service...");

            var response = await _questionService.DeleteQuestion(QId.ToUpper());

            if(response.Error != null)
            {
                log.LogError($"Error happened while deleting question {response.Error.Message}...");
                return new BadRequestObjectResult(response);
            }
            log.LogInformation("Quesions is Deleted succesfully...");
            return new OkObjectResult(response);
        }
    }
}

