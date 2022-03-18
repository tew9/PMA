using System;
using System.Collections.Generic;
using System.Text;

namespace Accelerator.Contracts
{
    public class QuestionsDTO
    {
        public string QID { get; set; }
        public int LogicID { get; set; } // may not need this, would route to backend logic but could be routed using Category
        public CategoryID CategoryID { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public QuestionDisplayTypes DisplayType { get; set; }
        public bool BoolQuestionType { get; set; }
        public IEnumerable<string> ListQuestionType { get; set; }
        public int TotalScore { get; set; }
        public int TotalPoint { get; set; }
    }

    //public class QuestionAnswer<T>
    //{
    //    public string QID { get; set; }
    //    public int Weight { get; set; }
    //    public T Answer { get; set; }
    //}

    public enum QuestionDisplayTypes
    {
        DropDown = 0,
        CheckBoxes = 1,
        TrueFalse = 2
    }
    public enum CategoryID
    {
        ApplicationSpec = 1,
        DevelopmentMethodology = 2,
        HostingEnvironment = 3,
        SourceControl = 4,
        BranchingStrategy = 5,
        ContinousIntegration = 6,
        Testing = 7,
        Containerization = 8,
        Database = 9,
        ContinousDelivery = 10,
        Security = 11,
        Monitoring = 12,
        Logging = 13,
        TeamCollaboration = 14,
    }
}
