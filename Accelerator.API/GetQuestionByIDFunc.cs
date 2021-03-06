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
    public class GetQuestionByIDFunc
    {
        #region constants
        private IQuestionService _questionService;
        #endregion
        public GetQuestionByIDFunc(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [FunctionName("GetQuestionByID")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "GetQuestionByID" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "QId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **QID** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(QuestionGetResponse), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "questions/{QId}")] HttpRequest req, string QId,
            ILogger log)
        {
            log.LogInformation("GetQuestionByID function is triggered...");

            if(string.IsNullOrEmpty(QId))
            {
                log.LogError($"Please provide QId parameter...");
                return new BadRequestResult();
            }

            log.LogInformation("Calling Questions service...");

            var response = await _questionService.GetQuestionById(QId.ToUpper());

            if(response.Error != null)
            {
                log.LogError($"Getting question returned error {response.Error.Message}...");
                return new BadRequestObjectResult(response);
            }
            log.LogInformation("Quesions is retrieved succesfully...");
            return new OkObjectResult(response);
        }
    }
}

