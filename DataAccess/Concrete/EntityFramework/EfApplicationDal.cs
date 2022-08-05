using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using Entities.Dto;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfApplicationDal : EfEntityRepositoryBase<Application, DataContext>, IApplicationDal
    {
        public List<ApplicationDetailDto> GetApplicationDetails()
        {
            using (var context = new DataContext())
            {
                var result = from t1 in context.Applications
                             join t2 in context.Candidates
                             on t1.CandidateId equals t2.Id

                             join t3 in context.JobAdverts
                             on t1.JobAdvertId equals t3.Id

                             select new ApplicationDetailDto
                             {
                                 Id = t1.Id,
                                 ApplicationDate = t1.ApplicationDate,
                                 CandidateId = t2.Id,
                                 CandidateFirstName = t2.FirstName,
                                 CandidateLastName = t2.LastName,
                                 CandidateFullName = $"{t2.FirstName} {t2.LastName}",
                                 JobAdvertId = t3.Id,
                                 PositionName = t3.PositionName,
                                 QualificationLevel = t3.QualificationLevel,
                                 PublishDate = t3.PublishDate,
                                 WorkTimeType = t3.WorkTimeType,
                                 WorkPlaceType = t3.WorkPlaceType,
                                 Deadline = t3.Deadline,
                                 IsDeleted = t1.IsDeleted
                             };
                return result.ToList();
            }
        }
    }
}
