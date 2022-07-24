using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionDal questionDal;
        public QuestionService(IQuestionDal questionDal)
        {
            this.questionDal = questionDal;
        }

        public void Add(Question question)
        {
            questionDal.Add(question);
        }

        public void Delete(int id)
        {
            var deletedEntity = questionDal.Get(x => x.Id == id);
            questionDal.SoftDelete(deletedEntity);
        }

        public List<Question> GetAll()
        {
            return questionDal.GetAll();
        }

        public Question GetById(int id)
        {
            return questionDal.Get(x => x.Id == id);
        }

        public void Update(Question question)
        {
            questionDal.Update(question);
        }
    }
}
