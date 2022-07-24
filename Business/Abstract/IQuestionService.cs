using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IQuestionService
    {
        void Add(Question question);
        void Update(Question question);
        void Delete(int id);
        List<Question> GetAll();
        Question GetById(int id);
    }
}
