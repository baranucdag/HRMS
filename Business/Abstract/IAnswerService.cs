using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IAnswerService
    {
        void Add(Answer answer);
        void Update(Answer answer);
        void Delete(Answer answer);
        List<Answer> GetAll();
        Answer GetById(int id);
        List<Answer> GetByQuestionId(int questionId);
    }
}
