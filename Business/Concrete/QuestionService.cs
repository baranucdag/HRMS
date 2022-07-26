using Business.Abstract;
using Business.Constans;
using Business.CrossCuttingConcerns.SecuredOperations;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;

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
            SecuredOperationTool securedOperation = new SecuredOperationTool("admin");
            questionDal.Add(question);
        }

        public void Delete(int id)
        {
            var deletedEntity = questionDal.Get(x => x.Id == id);
            questionDal.SoftDelete(deletedEntity);
        }
        public Question GetById(int id)
        {
            return questionDal.Get(x => x.Id == id);
        }

        public void Update(Question question)
        {
            questionDal.Update(question);
        }

        public ResultItem GetAll()
        {
            return new ResultItem(true, questionDal.GetAll(), Messages.DataListed);
        }

    }
}
