using Accelerator.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accelerator.API.Shared.Models
{
    public class Questions
    {
        [JsonProperty("questionID")]
        public string QID { get; set; }
        [JsonProperty("categoryID")]
        public CategoryID CategoryID { get; set; } //allows pulling different questions together as long as they belong to the same category
        [JsonProperty("questionDescription")]
        public string Description { get; set; }
        [JsonProperty("questionDisplayType")]
        public QuestionDisplayTypes DisplayType { get; set; }
        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("subCategory")]
        public string SubCategory { get; set; }
        [JsonProperty("answerType")]
        public AnswerDisplayType AnswerType { get; set; }
        [JsonProperty("totalPoints")]
        public int TotalPoint { get; set; }
        [JsonProperty("CreatedBy")]
        public UserDTO CreatedBy { get; set; }
        [JsonProperty("ModifiedBy")]
        public UserDTO ModifiedBy { get; set; }
        [JsonProperty("DateCreated")]
        public DateTime DateCreated { get; set; }
        [JsonProperty("DateModified")]
        public DateTime DateModified { get; set; }
    }
}


// {
// 		"questionId": "ATY",
// 		"logicId": 3,
// 		"shortDescription": "Application Technology",
// 		"description": "What are the technologies used for software development?",
// 		"category": "Application Spec",
// 		"subCategory": "",
// 		"displayType": "Check Boxes",
// 		"answers": {
// 			"selectedAnswer": [],
// 			"answerOptions": [
// 				{
// 					"value": "Java",
// 					"weight": 0
// 				},
// 				{
// 					"value": "ASP .NET",
// 					"weight": 0
// 				},
// 				{
// 					"value": "Angular JS",
// 					"weight": 0
// 				},
// 				{
// 					"value": "Python",
// 					"weight": 0
// 				},
// 				{
// 					"value": "Others",
// 					"weight": 0
// 				}
// 			]
// 		},
// 		"totalPoints": 10,
// 		"pointsScored": 0
// 	}