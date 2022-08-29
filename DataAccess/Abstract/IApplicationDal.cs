using Core.DataAccess;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IApplicationDal:IEntityRepository<Application>
    {
        List<ApplicationDto> GetApplicationDetails(Expression<Func<ApplicationDto, bool>> filter = null);
    }
}
