using Accelerator.API.Shared.Models;
using Accelerator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accelerator.API.Shared.Mappers
{
    public class QuestionMapper : IMapper<QuestionsDTO, Questions>
    {
        public QuestionsDTO Map(Questions from)
        {
            return new QuestionsDTO()
            {
                QID = from.QID,
                CategoryID = from.CategoryID,
                Description = from.Description,
                DisplayType = from.DisplayType,
                ShortDescription = from.ShortDescription,
                Category = from.Category,
                SubCategory = from.SubCategory,
                AnswerType = from.AnswerType,
                TotalPoint = from.TotalPoint
            };
        }

        public Questions Map(QuestionsDTO from)
        {
            return new Questions()
            {
                QID = from.QID,
                CategoryID = from.CategoryID,
                Description = from.Description,
                DisplayType = from.DisplayType,
                ShortDescription = from.ShortDescription,
                Category = from.Category,
                SubCategory = from.SubCategory,
                AnswerType = from.AnswerType,
                TotalPoint = from.TotalPoint
            };
        }
    }
}
