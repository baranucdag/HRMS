using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IQuestionService
    {
        void Add(Question question);
        List<Question> GetAll();
    }
}
