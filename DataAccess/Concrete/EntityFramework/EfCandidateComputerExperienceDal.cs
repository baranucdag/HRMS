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
    public class EfCandidateComputerExperienceDal : EfEntityRepositoryBase<CandidateComputerExperience, DataContext>, ICandidateComputerExperienceDal
    {
        public List<CandidateComputerExperienceDto> GetCandidateComputerExperienceById(Expression<Func<CandidateComputerExperienceDto, bool>> filter = null)
        {
            using (var context = new DataContext())
            {
                var result = from t1 in context.CandidateComputerExperiences
                             join t2 in context.Candidates
                             on t1.CandidateId equals t2.Id

                             select new CandidateComputerExperienceDto
                             {
                                 Id = t1.Id,
                                 CandidateFirstName = t2.FirstName,
                                 CandidateLastName = t2.LastName,
                                 CandidateFullName = $"{t2.FirstName} {t2.LastName}",
                                 CandidateId = t2.Id,
                                 Duration = t1.Duration,
                                 ExperienceType = t1.ExperienceType,
                                 LanguageOrProgram = t1.LanguageOrProgram,
                                 WorksDone = t1.WorksDone,
                                 IsDeleted = t1.IsDeleted
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList(); 
            }
        }

        public List<CandidateComputerExperienceDto> GetCandidateComputerExperienceDetails()
        {
            using (var context = new DataContext())
            {
                var result = from t1 in context.CandidateComputerExperiences
                             join t2 in context.Candidates
                             on t1.CandidateId equals t2.Id

                             select new CandidateComputerExperienceDto
                             {
                                 Id = t1.Id,
                                 CandidateFirstName = t2.FirstName,
                                 CandidateLastName = t2.LastName,
                                 CandidateFullName = $"{t2.FirstName} {t2.LastName}",
                                 CandidateId = t2.Id,
                                 Duration = t1.Duration,
                                 ExperienceType = t1.ExperienceType,
                                 LanguageOrProgram = t1.LanguageOrProgram,
                                 WorksDone = t1.WorksDone,
                                 IsDeleted = t1.IsDeleted
                             };
                return result.ToList();
            }
        }
    }
}
