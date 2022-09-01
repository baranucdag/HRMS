using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entites.Enums;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfApplicationDal : EfEntityRepositoryBase<Application, DataContext>, IApplicationDal
    {
        public List<ApplicationDto> GetApplicationDetails(Expression<Func<ApplicationDto, bool>> filter = null)
        {
            using (var context = new DataContext())
            {
                var result = from t1 in context.Applications
                             join t2 in context.Candidates
                             on t1.CandidateId equals t2.Id

                             join t3 in context.JobAdverts
                             on t1.JobAdvertId equals t3.Id

                             join t4 in context.Users
                             on t2.UserId equals t4.Id

                             select new ApplicationDto
                             {
                                 Id = t1.Id,
                                 ApplicationDate = t1.ApplicationDate,
                                 CandidateId = t2.Id,
                                 JobAdvertId = t3.Id,
                                 PositionName = t3.PositionName,
                                 QualificationLevel = t3.QualificationLevel,
                                 PublishDate = t3.PublishDate,
                                 WorkTimeType = t3.WorkTimeType.ToString(),
                                 WorkPlaceType = t3.WorkPlaceType.ToString(),
                                 ApplicationStatus = t1.ApplicationStatus.ToString(),
                                 HasEmailSent = t1.HasEmailSent,
                                 PrevApplicationStatus = t1.PrevApplicationStatus.ToString(),
                                 Deadline = t3.Deadline,
                                 CandidateFirstName = t4.FirstName,
                                 CandidateLastName = t4.LastName,
                                 CraetedAt = t1.CreatedAt,
                                 IsDeleted = t1.IsDeleted
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
