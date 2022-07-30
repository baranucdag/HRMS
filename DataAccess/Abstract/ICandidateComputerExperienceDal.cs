using Core.DataAccess;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface ICandidateComputerExperienceDal : IEntityRepository<CandidateComputerExperience>
    {
        List<CandidateComputerExperienceDto> GetCandidateComputerExperienceDetails();
        List<CandidateComputerExperienceDto> GetCandidateComputerExperienceById(Expression<Func<CandidateComputerExperienceDto, bool>> filter = null);
    }
}
