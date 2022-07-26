using Core.Utilities.Result;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IQuestionService
    {
        void Add(Question question);
        void Update(Question question);
        void Delete(int id);
        ResultItem GetAll();
        Question GetById(int id);
    }
}
