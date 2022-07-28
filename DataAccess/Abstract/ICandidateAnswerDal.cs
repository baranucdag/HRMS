using Core.DataAccess;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface ICandidateAnswerDal : IEntityRepository<CandidateAnswer>
    {
        public List<CandidateAnswerDto> GetCandidateAnswerDetail(Expression<Func<CandidateAnswerDto, bool>> filter = null);
    }
}
