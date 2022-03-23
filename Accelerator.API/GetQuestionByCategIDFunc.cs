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
    public class GetQuestionByCategoryIDFunc
    {
        #region constants
        private IQuestionService _questionService;
        #endregion
        public GetQuestionByCategoryIDFunc(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [FunctionName("GetQuestionByCategoryID")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "GetQuestionByCategoryID" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "categoryID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **categoryID** parameter, Insert **categoryID** as number, use below Mapping" +
            "ApplicationSpec = 1, "+
            "DevelopmentMethodology = 2, HostingEnvironment = 3, SourceControl = 4, BranchingStrategy = 5, "+
            "ContinousIntegration = 6, Testing = 7, Containerization = 8, Database = 9, ContinousDelivery = 10, "+
            "Security = 11, Monitoring = 12, Logging = 13, TeamCollaboration = 14, ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(QuestionGetResponse), Description = "The OK response")]
        public async Task<IActionResult> QuestionByCat(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "questions/category/{categoryID}")] HttpRequest req, string categoryID,
            ILogger log)
        {
            log.LogInformation("GetQuestionByID function is triggered...");

            if(string.IsNullOrEmpty(categoryID))
            {
                log.LogError($"Please provide categoryID parameter...");
                return new BadRequestResult();
            }
            try
            {
                Int32.Parse(categoryID);
            }
            catch(Exception)
            {
                log.LogError($"Please provide correct categoryID as an int representation of the category...");
                return new BadRequestResult();
            }

            log.LogInformation("Calling Questions service...");

            var response = await _questionService.GetQuestionsByCategoryID(Int32.Parse(categoryID));

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

