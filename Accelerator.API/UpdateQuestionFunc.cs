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
    public class UpdateQuestionFunc
    {
        #region constants
        private IQuestionService _questionService;
        #endregion
        public UpdateQuestionFunc(IQuestionService questionService)
        {
            _questionService = questionService;
        }


        [FunctionName("UpdateQuestionFunc")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "UpdateQuestionFunc" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Questions), Description = "The **Question properties** parameters, **questionDisplayType** is number type, use below Mapping DropDown = 0, " +
            "CheckBoxes = 1, TrueFalse = 2")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(QuestionPostResponse), Description = "The OK response")]
        public async Task<IActionResult> RunAddQuestion(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "questions")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("UpdateQuestion func is triggered...");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var question = JsonConvert.DeserializeObject<Questions>(requestBody);

            log.LogInformation("Calling AddQuestion service...");
            if(question.QID.Length != 3)
            {
                log.LogError($"Please provide all the required questionaire fields...");
                return new BadRequestResult();
            }

            var response = await _questionService.UpdateQuestion(question.QID, question);

            if(response.Error != null)
            {
                log.LogError($"Updating question returned error {response.Error.Message}...");
                return new BadRequestObjectResult(response);
            }
            log.LogInformation("Quesion is Updated succesfully...");
            log.LogInformation("Quesion is Updated succesfully...");
            return new OkObjectResult(response);
        }
    }
}

