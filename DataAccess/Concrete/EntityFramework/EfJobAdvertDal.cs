using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfJobAdvertDal : EfEntityRepositoryBase<JobAdvert, DataContext>, IJobAdvertDal
    {

        //Get JobAdverts dto
        public List<JobAdvertDto> GetJobAdvertDtos()
        {
            List<JobAdvertDto> result = new List<JobAdvertDto>();
            using (var context = new DataContext())
            {

                var query = context.Set<JobAdvert>().Select(x => new JobAdvertDto
                {
                    Id = x.Id,
                    Description = x.Description,
                    Deadline = x.Deadline,
                    PositionName = x.PositionName,
                    PublishDate = x.PublishDate,
                    QualificationLevel = x.QualificationLevel,
                    Status = x.Status,
                    DepartmentText = x.Department.ToString(),
                    Department = Convert.ToInt32(x.Department),
                    WorkPlaceTypeText = x.WorkPlaceType.ToString(),
                    WorkPlaceType = Convert.ToInt32(x.WorkPlaceType),
                    WorkTimeTypeText = x.WorkTimeType.ToString(),
                    WorkTimeType = Convert.ToInt32(x.WorkTimeType),
                    IsDeleted = x.IsDeleted
                });
                result = query.ToList();

            }
            return result;
        }

        public JobAdvertDto GetJobAdvertDto(Expression<Func<JobAdvertDto, bool>> filter = null)
        {
            JobAdvertDto result = new JobAdvertDto();
            using (var context = new DataContext())
            {
                var query = context.Set<JobAdvert>().Where(x => x.IsDeleted == 0).Select(x => new JobAdvertDto
                {
                    Id = x.Id,
                    Description = x.Description,
                    Deadline = x.Deadline,
                    PositionName = x.PositionName,
                    PublishDate = x.PublishDate,
                    QualificationLevel = x.QualificationLevel,
                    Status = x.Status,
                    DepartmentText = x.Department.ToString(),
                    Department = Convert.ToInt32(x.Department),
                    WorkPlaceTypeText = x.WorkPlaceType.ToString(),
                    WorkPlaceType = Convert.ToInt32(x.WorkPlaceType),
                    WorkTimeTypeText = x.WorkTimeType.ToString(),
                    WorkTimeType = Convert.ToInt32(x.WorkTimeType),
                    IsDeleted = x.IsDeleted
                });
                result = filter == null ? query.FirstOrDefault() : query.Where(filter).FirstOrDefault();

            }
            return result;
        }

    }
}
