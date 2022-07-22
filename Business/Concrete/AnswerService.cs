using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerDal answerDal;

        public AnswerService(IAnswerDal answerDal)
        {
            this.answerDal = answerDal;
        }
        public void Add(Answer answer)
        {
            answerDal.Add(answer);
        }

        public void Delete(Answer answer)
        {
            answerDal.SoftDelete(answer);
        }

        public List<Answer> GetAll()
        {
            return answerDal.GetAll();
        }

        public Answer GetById(int id)
        {
            return answerDal.Get(x => x.Id == id);
        }

        public List<Answer> GetByQuestionId(int questionId)
        {
            return answerDal.GetAll(x => x.QuestionId == questionId);
        }

        public void Update(Answer answer)
        {
            answerDal.Update(answer);
        }
    }
}
