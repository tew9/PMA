using Accelerator.API.Shared.Models;
using Accelerator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accelerator.API.Shared.Services
{
    public interface IQuestionService 
    {
        public Task<QuestionPostResponse> AddQuestion(Questions question);
        public Task<QuestionGetResponse> GetQuestionById(string QID);
        public Task<QuestionGetResponse> GetQuestions();
        public Task<QuestionPostResponse> DeleteQuestion(string qId);
        public Task<QuestionGetResponse> GetQuestionsByCategoryID(int QID);
        public Task<QuestionPostResponse> UpdateQuestion(string QID, Questions question);
    }
}
