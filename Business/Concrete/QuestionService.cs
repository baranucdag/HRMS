using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<Question> GetAll()
        {
            return questionDal.GetAll();
        }
    }
}
