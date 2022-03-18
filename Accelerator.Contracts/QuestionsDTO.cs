using Accelerator.Contracts.Base;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accelerator.Contracts
{
    public class QuestionsDTO : BaseEntityDTO
    {
        public int LogicID { get; set; } // may not need this, would route to backend logic but could be routed using Category
        [BsonElement("CategoryID")]
        public CategoryID CategoryID { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("ShortDescription")]
        public string ShortDescription { get; set; }
        [BsonElement("Category")]
        public string Category { get; set; }
        [BsonElement("SubCategory")]
        public string SubCategory { get; set; }
        [BsonElement("DisplayType")]
        public QuestionDisplayTypes DisplayType { get; set; }
        [BsonElement("AnswerType")]
        public AnswerDisplayType AnswerType { get; set; }
        [BsonElement("TotalScore")]
        public int TotalScore { get; set; }
        [BsonElement("TotalPoint")]
        public int TotalPoint { get; set; }
    }

    //public class QuestionAnswer<T>
    //{
    //    public string QID { get; set; }
    //    public int Weight { get; set; }
    //    public T Answer { get; set; }
    //}

    public class AnswerDisplayType
    {
        public bool BoolType { get; set; }
        public IEnumerable<string> ListType { get; set; }
    }

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
