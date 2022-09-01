using Core.DataAccess;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface ICandidateDal : IEntityRepository<Candidate>
    {
        List<CandidateDto> GetCandidateDetails();
        CandidateDto GetCandidateDetail(Expression<Func<CandidateDto, bool>> filter = null);
    }
}
