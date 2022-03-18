using Accelerator.API.Shared.Mappers;
using Accelerator.API.Shared.Models;
using Accelerator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accelerator.API.Shared.Services
{
    public class QuestionService : IQuestionService
    {
        IMapper<QuestionsDTO, Questions> _questionMapper;

        static List<QuestionsDTO> questions = new List<QuestionsDTO>();

        public QuestionService(IMapper<QuestionsDTO, Questions> questionMapper)
        {
            _questionMapper = questionMapper;
        }
        public async Task<QuestionPostResponse> AddQuestion(Questions question)
        {
            try
            {
                questions.Add(_questionMapper.Map(question));
                Info info = new Info()
                {
                    Message = "Question Inserted Successfuly",
                    Source = "AddQuestion"
                };
                return new QuestionPostResponse()
                {
                    Info = info,
                    Status = "succeed",
                };
            }
            catch (Exception e)
            {
                return new QuestionPostResponse()
                {
                    Error = new Error() {Message = $"Error happened, {e.Message}", Type="AddQuestion" },
                    Status = "succeed",
                };
            }
        }

        public Task<QuestionGetResponse> GetQuestion(string QID)
        {
            throw new NotImplementedException();
        }

        public async Task<QuestionGetResponse> GetQuestions()
        {
            try
            {
                var quests = new List<Questions>();
                foreach (var q in questions)
                {
                    quests.Add(_questionMapper.Map(q));
                }

                return new QuestionGetResponse()
                {
                    Status = "success",
                    Count = questions.Count,
                    Values = quests,
                    Info = new Info() { Message = "got all the questions successfuly", Source = "GetQuestions" }
                };
            }
            catch (Exception e)
            {
                return  new QuestionGetResponse()
                {
                    Status = "Error",
                    Error = new Error() { Message = "Error happened while retrieving all questions", Type = "GetQuestions" }
                };
            } 
        }
    }
}
