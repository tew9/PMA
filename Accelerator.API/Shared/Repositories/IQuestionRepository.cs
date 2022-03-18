using Accelerator.API.Shared.Repositories.Base;
using Accelerator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accelerator.API.Shared.Repositories
{
    public interface IQuestionRepository : IBaseRepository<QuestionsDTO>
    {
        public Task<QuestionsDTO> GetQuestionsByIdAsync(string id);
        public Task<IEnumerable<QuestionsDTO>> GetAllQuestionsAsync();
        Task<IEnumerable<QuestionsDTO>> GetQuestionsByCategoryIDAsync(int catID);
    }
}
