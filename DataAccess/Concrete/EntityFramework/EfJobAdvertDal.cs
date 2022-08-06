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
        public List<JobAdvertDto> GetJobAdvertDtos()
        {
            List<JobAdvertDto> result = new List<JobAdvertDto>();
            using (var context = new DataContext())
            {
                var jobAdverts = context.Set<JobAdvert>().ToList();
                foreach (var item in jobAdverts)
                {
                    JobAdvertDto jobAdvertDto = new JobAdvertDto();
                    jobAdvertDto.Id = item.Id;
                    jobAdvertDto.PositionName = item.PositionName;
                    jobAdvertDto.Status = item.Status;  
                    jobAdvertDto.Description = item.Description;    
                    jobAdvertDto.Deadline = item.Deadline;
                    jobAdvertDto.IsDeleted = item.IsDeleted;
                    jobAdvertDto.PublishDate = item.PublishDate;
                    jobAdvertDto.QualificationLevel = item.QualificationLevel;
                    jobAdvertDto.Status = item.Status;
                    jobAdvertDto.WorkTimeType = item.WorkTimeType;
                    jobAdvertDto.WorkPlaceType = item.WorkPlaceType;
                    result.Add(jobAdvertDto);
                }
            }
            return result; 
        }
       
    }
}
