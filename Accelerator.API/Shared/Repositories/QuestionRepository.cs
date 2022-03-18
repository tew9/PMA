using Accelerator.API.Shared.Models;
using Accelerator.API.Shared.Repositories.Base;
using Accelerator.Contracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accelerator.API.Shared.Repositories
{
    public class QuestionRepository : BaseRepository<QuestionsDTO>, IQuestionRepository
    {
        public QuestionRepository(
            IMongoClient mongoClient,
            IClientSessionHandle clientSessionHandle) : base(mongoClient, clientSessionHandle, "questionaires")
        {
        }

        public async Task<QuestionsDTO> GetQuestionsByIdAsync(string id)
        {
            var filter = Builders<QuestionsDTO>.Filter.Eq(s => s.QID, id);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<QuestionsDTO>> GetQuestionsByCategoryIDAsync(int catID)
        {
            var filter = Builders<QuestionsDTO>.Filter.Where(s => (int)s.CategoryID == catID);
            return await Collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<QuestionsDTO>> GetAllQuestionsAsync() =>
            await Collection.AsQueryable().ToListAsync();
    }
}
