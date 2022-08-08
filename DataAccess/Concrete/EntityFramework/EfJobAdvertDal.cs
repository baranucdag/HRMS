using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using Entities.Dto;
using System.Collections.Generic;
using System.Linq;

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
                    WorkPlaceType = x.WorkPlaceType.ToString(),
                    WorkTimeType = x.WorkTimeType.ToString(),
                    IsDeleted = x.IsDeleted
                });
                result = query.ToList();

            }
            return result;
        }

    }
}
