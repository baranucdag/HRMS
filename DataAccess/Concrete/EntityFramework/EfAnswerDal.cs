using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using Entities.Dto;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAnswerDal : EfEntityRepositoryBase<Answer, DataContext>, IAnswerDal
    {
        public List<AnswerDetailDto> GetAnswerDetails()
        {
            using (var context = new DataContext())
            {
                var result = from t1 in context.Answers
                             join t2 in context.Questions
                             on t1.QuestionId equals t2.Id

                             select new AnswerDetailDto
                             {
                                 Id = t1.Id,
                                 QuestionId = t2.Id,
                                 QuestionText = t2.Text,
                                 Text = t1.Text
                             };
                return result.ToList();
            }
        }
    }
}
