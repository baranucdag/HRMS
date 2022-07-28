using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCandidateAnswerDal : EfEntityRepositoryBase<CandidateAnswer, DataContext>, ICandidateAnswerDal
    {
        public List<CandidateAnswerDto> GetCandidateAnswerDetail(Expression<Func<CandidateAnswerDto, bool>> filter = null)
        {
            using (var context = new DataContext())
            {
                var result = from t1 in context.CandidateAnswers
                             join t2 in context.Candidates
                             on t1.CandidateId equals t2.Id

                             join t3 in context.Answers
                             on t1.AnswerId equals t3.Id

                             select new CandidateAnswerDto
                             {
                                 Id = t1.Id,
                                 AnswerId = t3.Id,
                                 AnswerText = t3.Text,
                                 AnswerValue = t1.AnswerValue,
                                 CandidateFirstName = t2.FirstName,
                                 CandidateLastName = t2.LastName,
                                 CandidateFullName = $"{t2.FirstName} {t2.LastName}",
                                 CandidateId = t2.Id,
                                 IsDeleted = t1.IsDeleted

                             };
                return result.ToList();
            }
        }
    }
}
