using Accelerator.API.Shared.Mappers;
using Accelerator.API.Shared.Models;
using Accelerator.Contracts;
using System;
using System.Collections.Generic;
using Accelerator.API.Shared.Repositories;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Accelerator.API.Shared.Services
{
    public class QuestionService : IQuestionService
    {
        IMapper<QuestionsDTO, Questions> _questionMapper;
        private IQuestionRepository _questionRepository;
        private IClientSessionHandle _clientSessionHandle;

        static List<QuestionsDTO> questions = new List<QuestionsDTO>();

        public QuestionService(IMapper<QuestionsDTO, Questions> questionMapper,
            IQuestionRepository questionRepository, IClientSessionHandle clientSessionHandle) =>
        (_questionMapper, _questionRepository, _clientSessionHandle) = (questionMapper, questionRepository, clientSessionHandle);

        #region addQuestion
        public async Task<QuestionPostResponse> AddQuestion(Questions question)
        {

            if(string.IsNullOrEmpty(question.QID) || question.QID.Length != 3)
            {
                return new QuestionPostResponse()
                {
                    Error = new Error() { Message = $"Incomplete Request, Please provide the correct question payload", Type = "AddQuestion" },
                    Status = "Failed",
                };
            }
            //start db transaction
            _clientSessionHandle.StartTransaction();

            try
            {

                await _questionRepository.InsertAsync(_questionMapper.Map(question));
                await _clientSessionHandle.CommitTransactionAsync();
                
                Info info = new Info()
                {
                    Message = "Question Inserted Successfuly",
                    Source = "AddQuestion"
                };
                return new QuestionPostResponse()
                {
                    Info = info,
                    Status = "success",
                };
            }
            catch (Exception e)
            {
                await _clientSessionHandle.AbortTransactionAsync();

                return new QuestionPostResponse()
                {
                    Error = new Error() {Message = $"Error happened, {e.Message}", Type="AddQuestion" },
                    Status = "Failed",
                };
            }
        }
        #endregion

        #region updateQuestion
        public async Task<QuestionPostResponse> UpdateQuestion(string QID, Questions question)
        {
            if (string.IsNullOrEmpty(question.QID))
            {
                return new QuestionPostResponse()
                {
                    Error = new Error() { Message = $"Incomplete Request, Please provide the question Id you want to update", Type = "UpdateQuestion" },
                    Status = "Failed",
                };
            }
            //start db transaction
            _clientSessionHandle.StartTransaction();

            try
            {
                var questionDto = _questionMapper.Map(question);
                questionDto.SetId(QID);

                await _questionRepository.UpdateAsync(questionDto);
                await _clientSessionHandle.CommitTransactionAsync();

                Info info = new Info()
                {
                    Message = $"Question with ID {QID} is Updated Successfuly",
                    Source = "UpdateQuestion"
                };
                return new QuestionPostResponse()
                {
                    Info = info,
                    Status = "success",
                };
            }
            catch (Exception e)
            {
                await _clientSessionHandle.AbortTransactionAsync();

                return new QuestionPostResponse()
                {
                    Error = new Error() { Message = $"Error happened while updating question, {e.Message}", Type = "UpdateQuestion" },
                    Status = "Failed",
                };
            }
        }
        #endregion

        #region deleteQuestion
        public async Task<QuestionPostResponse> DeleteQuestion(string qId)
        {
            if (string.IsNullOrEmpty(qId))
            {
                return new QuestionPostResponse()
                {
                    Error = new Error() { Message = $"Incomplete Request, Please provide a correct QID", Type = "DeleteQuestion" },
                    Status = "Failed",
                };
            }

            //start db transaction
            _clientSessionHandle.StartTransaction();
            try
            {
                await _questionRepository.DeleteAsync(qId);
                await _clientSessionHandle.CommitTransactionAsync();

                Info info = new Info()
                {
                    Message = "Question is Deleted Successfuly",
                    Source = "DeleteQuestion"
                };
                return new QuestionPostResponse()
                {
                    Info = info,
                    Status = "success",
                };
            }
            catch (Exception e)
            {
                await _clientSessionHandle.AbortTransactionAsync();

                return new QuestionPostResponse()
                {
                    Error = new Error() { Message = $"Failed to delete a question, {e.Message}", Type = "DeleteQuestion" },
                    Status = "Failed",
                };
            }
        }
        #endregion

        #region getQuestionByID
        public async Task<QuestionGetResponse> GetQuestionById(string QID)
        {
            if (string.IsNullOrEmpty(QID))
            {
                return new QuestionGetResponse()
                {
                    Error = new Error() { Message = $"Incomplete Request, Please provide a correct QID", Type = "GetQuestionByID" },
                    Status = "Failed",
                };
            }

            try
            {
                var question = await _questionRepository.GetQuestionsByIdAsync(QID);

                var q = new List<Questions>();
                q.Add(_questionMapper.Map(question));
          
                return new QuestionGetResponse()
                {
                    Status = "success",
                    Count = q.Count,
                    Values = q,
                    Info = new Info() { Message = " Retrieved the question successfuly", Source = "GetQuestionByID" }
                };
            }
            catch (Exception e)
            {
                return new QuestionGetResponse()
                {
                    Status = "Error",
                    Error = new Error() { Message = $"Error happened while retrieving the question, {e.Message}", Type = "GetQuestionById" }
                };
            }
        }
        #endregion

        #region getQuestiosByCategoryID
        public async Task<QuestionGetResponse> GetQuestionsByCategoryID(int CatID)
        {

            try
            {
                var questions = await _questionRepository.GetQuestionsByCategoryIDAsync(CatID);

                var quests = new List<Questions>();
                foreach (var q in questions)
                {
                    quests.Add(_questionMapper.Map(q));
                }

                return new QuestionGetResponse()
                {
                    Status = "success",
                    Count = quests.Count,
                    Values = quests,
                    Info = new Info() { Message = " Retrieved the questions successfuly", Source = "GetQuestionByCategoryID" }
                };
            }
            catch (Exception e)
            {
                return new QuestionGetResponse()
                {
                    Status = "Error",
                    Error = new Error() { Message = $"Error happened while retrieving the questions, {e.Message}", Type = "GetQuestionByCategoryId" }
                };
            }
        }
        #endregion
       
        #region GetAllQuestions
        public async Task<QuestionGetResponse> GetQuestions()
        {
            try
            {
                var questions = await _questionRepository.GetAllQuestionsAsync();
                var quests = new List<Questions>();
                foreach (var q in questions)
                {
                    quests.Add(_questionMapper.Map(q));
                }

                return new QuestionGetResponse()
                {
                    Status = "success",
                    Count = quests.Count,
                    Values = quests,
                    Info = new Info() { Message = "got all the questions successfuly", Source = "GetQuestions" }
                };
            }
            catch (Exception e)
            {
                return  new QuestionGetResponse()
                {
                    Status = "Error",
                    Error = new Error() { Message = $"Error happened while retrieving all questions, {e.Message}", Type = "GetQuestions" }
                };
            } 
        }
        #endregion
    }
}
