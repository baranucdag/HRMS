using Core.DataAccess;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IJobAdvertDal : IEntityRepository<JobAdvert>
    {
        List<JobAdvertDto> GetJobAdvertDtos();
        JobAdvertDto GetJobAdvertDto(Expression<Func<JobAdvertDto, bool>> filter = null);
    }
}
