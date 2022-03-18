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
        public Task<QuestionGetResponse> GetQuestion(string QID);
        public Task<QuestionGetResponse> GetQuestions();
    }
}
