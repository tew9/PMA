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
    public class AddQuestionFunc
    {
        #region constants
        private IQuestionService _questionService;
        #endregion
        public AddQuestionFunc(IQuestionService questionService)
        {
            _questionService = questionService;
        }


        [FunctionName("AddQuestion")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "AddQuestion" })]
        //[OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        //[OpenApiParameter(name: "name", In = ParameterLocation.Path, Required = false, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Questions), Description = "The **Question properties** parameters")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(QuestionPostResponse), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "questions")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Question Post is triggered...");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var question = JsonConvert.DeserializeObject<Questions>(requestBody);
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            log.LogInformation("Calling AddQuestion service...");
            var response = await _questionService.AddQuestion(question);

            if(response.Error != null)
            {
                log.LogError($"Adding question returned error {response.Error.Message}...");
                return new BadRequestObjectResult(response);
            }
            log.LogInformation("Quesion is added succesfully...");
            return new OkObjectResult(response);
        }
    }
}

