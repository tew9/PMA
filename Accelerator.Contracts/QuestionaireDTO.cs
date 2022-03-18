using System;
using System.Collections.Generic;
using System.Text;

namespace Accelerator.Contracts
{
    /// <summary>
    /// This questionaires will be either a completed or incomplete survey that'll be saved
    /// </summary>
    /// <typeparam name="T"> represent the type of question's anwers, can be either bool, or IEnumerable</typeparam>
    public class QuestionaireDTO<T>
    {
        public int SureveyId {get; set;}
        public  string Name {get; set;}
        public double AssessmentScore {get; set;}
        public string CreatedBy {get; set;}
        public DateTime DateCreated {get; set;}
        public string ModifiedBy {get; set;}
        public DateTime DateModified {get; set;}
        public IEnumerable<QuestionsDTO> QuestionResponse { get; set; }
        public SurveyReports SurveyReports { get; set; }
    }
}


public class SurveyReports
{
    // current and preffered implementation (gray-yellow and red-blue)
    public IEnumerable<ProductReport> ProductReports {get; set; }
    // blue-black category
    public IEnumerable<AssessmentSummaryReport> AssessmentSummaryReport { get; set; }
}

public class AssessmentSummaryReport {
    public string Name { get; set; }
    public IEnumerable<String> Value { get; set; }
}

public class ProductReport
{
    public string Name { get; set; }
    public IEnumerable<string> CurrentValue { get; set; }
    public IEnumerable<string> PreferredValue { get; set; }
    
}


//   surveyReports:[{
//         assessmentSummaryReport: [{
//             name: String,
//             value: [String]
//         }],
//         currentProductReport: [{
//             name: String,
//             value: [String]
//         }],
//         preferredProductReport: [{
//             name: String,
//             value: [String]
//         }]
//     }],

/*
 This is based on assumption that the assesmentsummary is a different category, and not a sub category 
 of plan, build, test ... abstracted category
 SurveyReport example
{
   ProductReport
  {
    name = "test",
    currentValues = {"asdfadfsa", "asdfafasdf", ""}
    PreferredValue = {"", "", ""}
  }

  AssesmentSumary (AssessmentReport)
  {
     name: logging,
     values: {"asdfadfasf",""}
  }

}
*/


