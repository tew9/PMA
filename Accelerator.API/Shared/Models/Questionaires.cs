using Accelerator.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accelerator.API.Shared.Models
{
    /// <summary>
    /// This model is intended map user input responses of the questions as xcell depicted
    /// </summary>
    /// <typeparam name="T">Type will depends on the type of the question</typeparam>
    public class Questionaires
    {
        [JsonProperty("surveyID")]
        public int SureveyId { get; set; }
        [JsonProperty("surveyName")]
        public string Name { get; set; }
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }
        [JsonProperty("dateCreated")]
        public DateTime DateCreated { get; set; }
        [JsonProperty("modifiedBy")]
        public string ModifiedBy { get; set; }
        [JsonProperty("dateModified")]
        public DateTime DateModified { get; set; }

        [JsonProperty("questionResponses")]
        public IEnumerable<Questions> QuestionResponses { get; set; }
    }    
}

