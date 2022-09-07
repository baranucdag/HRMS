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
    public class EfCandidateDal : EfEntityRepositoryBase<Candidate, DataContext>, ICandidateDal
    {
        public List<CandidateDto> GetCandidateDetails()
        {
            using (var context = new DataContext())
            {
                var result = from t1 in context.Candidates
                             join t2 in context.Users
                             on t1.UserId equals t2.Id

                             select new CandidateDto
                             {
                                 Id = t1.Id,
                                 UserId = t2.Id,
                                 Adress = t1.Adress,
                                 ApplicationReasonDetail = t1.ApplicationReasonDetail,
                                 BloudGroup = t1.BloudGroup,
                                 CanTravel = t1.CanTravel,
                                 CommonAquaintances = t1.CommonAquaintances,
                                 CriminalRecordReason = t1.CriminalRecordReason,
                                 CvPath = t1.CvPath,
                                 DateOfBirth = t1.DateOfBirth,
                                 DeutchReadingLevel = t1.DeutchReadingLevel,
                                 DeutchSpeakingLevel = t1.DeutchSpeakingLevel,
                                 DeutchWritingLevel = t1.DeutchWritingLevel,
                                 DisabilityDetail = t1.DisabilityDetail,
                                 DrivingLicenceInformation = t1.DrivingLicenceInformation,
                                 EMail = t2.Email,
                                 EnglishReadingLevel = t1.EnglishReadingLevel,
                                 EnglishSpeakingLevel = t1.EnglishSpeakingLevel,
                                 EnglishWritingLevel = t1.EnglishWritingLevel,
                                 ExpectedSalary = t1.ExpectedSalary,
                                 FatherCurrentJob = t1.FatherCurrentJob,
                                 FatherName = t1.FatherName,
                                 FatherProfession = t1.FatherProfession,
                                 FirstName = t2.FirstName,
                                 FrenchReadingLevel = t1.FrenchReadingLevel,
                                 FrenchSpeakingLevel = t1.FrenchSpeakingLevel,
                                 FrenchWritingLevel = t1.FrenchWritingLevel,
                                 Gender = t1.Gender.ToString(),
                                 HadMajorSurgery = t1.HadMajorSurgery,
                                 HasCriminalRecord = t1.HasCriminalRecord,
                                 HasDisability = t1.HasDisability,
                                 HasLawSuit = t1.HasLawSuit,
                                 HasMentalPhysicalProblem = t1.HasMentalPhysicalProblem,
                                 HasValidPassport = t1.HasValidPassport,
                                 HighSchoolEndDate = t1.HighSchoolEndDate,
                                 HighSchoolStartDate = t1.HighSchoolStartDate,
                                 HomePhoneNumber = t1.HomePhoneNumber,
                                 IsMarried = t1.IsMarried,
                                 JobForApplied = t1.JobForApplied,
                                 LastName = t2.LastName,
                                 LastPositionAdditionalBenefits = t1.LastPositionAdditionalBenefits,
                                 LastPositionDepartment = t1.LastPositionDepartment,
                                 LastPositionDescription = t1.LastPositionDescription,
                                 LastPositionManagerTitle = t1.LastPositionManagerTitle,
                                 LastPositionName = t1.LastPositionName,
                                 LawSuitReason = t1.LawSuitReason,
                                 MajorSurgeyDetail = t1.MajorSurgeryDetail,
                                 MasterEndDate = t1.MasterEndDate,
                                 MasterStartDate = t1.MasterStartDate,
                                 MentalPyhsicalProblemDetail = t1.MentalPhysicalProblemDetail,
                                 MiddleSchoolEndDate = t1.MiddleSchoolEndDate,
                                 MiddleSchoolStartDate = t1.MiddleSchoolStartDate,
                                 MilitaryInformation = t1.MilitaryInformation,
                                 MotherCurrentJob = t1.MotherCurrentJob,
                                 MotherName = t1.MotherName,
                                 MotherProfession = t1.MotherProfession,
                                 PhoneNumber = t1.PhoneNumber,
                                 PlaceOfBirth = t1.PlaceOfBirth,
                                 PossibleStartDate = t1.PossibleStartDate,
                                 PrimarySchoolEndDate = t1.PrimarySchoolEndDate,
                                 PrimarySchoolStartDate = t1.PrimarySchoolStartDate,
                                 Profession = t1.Profession,
                                 SpouseCurrentJob = t1.SpouseCurrentJob,
                                 SpouseName = t1.SpouseName,
                                 SpouseProfession = t1.SpouseProfession,
                                 SucscribedProfessionalOrganizations = t1.SucscribedProfessionalOrganizations,
                                 SucscribedSociety = t1.SucscribedSociety,
                                 SucscribedSporClubs = t1.SucscribedSporClubs,
                                 SucscribedUnions = t1.SucscribedUnions,
                                 UniversityEndDate = t1.UniversityEndDate,
                                 UniversityStartDate = t1.UniversityStartDate,
                                 IsDeleted = t1.IsDeleted,

                             };
                return result.ToList();
            }
        }
        public CandidateDto GetCandidateDetail(Expression<Func<CandidateDto, bool>> filter = null)
        {
            using (var context = new DataContext())
            {
                var result = from t1 in context.Candidates
                             join t2 in context.Users
                             on t1.UserId equals t2.Id

                             select new CandidateDto
                             {
                                 Id = t1.Id,
                                 UserId = t2.Id,
                                 Adress = t1.Adress,
                                 ApplicationReasonDetail = t1.ApplicationReasonDetail,
                                 BloudGroup = t1.BloudGroup,
                                 CanTravel = t1.CanTravel,
                                 CommonAquaintances = t1.CommonAquaintances,
                                 CriminalRecordReason = t1.CriminalRecordReason,
                                 CvPath = t1.CvPath,
                                 DateOfBirth = t1.DateOfBirth,
                                 DeutchReadingLevel = t1.DeutchReadingLevel,
                                 DeutchSpeakingLevel = t1.DeutchSpeakingLevel,
                                 DeutchWritingLevel = t1.DeutchWritingLevel,
                                 DisabilityDetail = t1.DisabilityDetail,
                                 DrivingLicenceInformation = t1.DrivingLicenceInformation,
                                 EMail = t2.Email,
                                 EnglishReadingLevel = t1.EnglishReadingLevel,
                                 EnglishSpeakingLevel = t1.EnglishSpeakingLevel,
                                 EnglishWritingLevel = t1.EnglishWritingLevel,
                                 ExpectedSalary = t1.ExpectedSalary,
                                 FatherCurrentJob = t1.FatherCurrentJob,
                                 FatherName = t1.FatherName,
                                 FatherProfession = t1.FatherProfession,
                                 FirstName = t2.FirstName,
                                 FrenchReadingLevel = t1.FrenchReadingLevel,
                                 FrenchSpeakingLevel = t1.FrenchSpeakingLevel,
                                 FrenchWritingLevel = t1.FrenchWritingLevel,
                                 Gender = t1.Gender.ToString(),
                                 HadMajorSurgery = t1.HadMajorSurgery,
                                 HasCriminalRecord = t1.HasCriminalRecord,
                                 HasDisability = t1.HasDisability,
                                 HasLawSuit = t1.HasLawSuit,
                                 HasMentalPhysicalProblem = t1.HasMentalPhysicalProblem,
                                 HasValidPassport = t1.HasValidPassport,
                                 HighSchoolEndDate = t1.HighSchoolEndDate,
                                 HighSchoolStartDate = t1.HighSchoolStartDate,
                                 HomePhoneNumber = t1.HomePhoneNumber,
                                 IsMarried = t1.IsMarried,
                                 JobForApplied = t1.JobForApplied,
                                 LastName = t2.LastName,
                                 LastPositionAdditionalBenefits = t1.LastPositionAdditionalBenefits,
                                 LastPositionDepartment = t1.LastPositionDepartment,
                                 LastPositionDescription = t1.LastPositionDescription,
                                 LastPositionManagerTitle = t1.LastPositionManagerTitle,
                                 LastPositionName = t1.LastPositionName,
                                 LawSuitReason = t1.LawSuitReason,
                                 MajorSurgeyDetail = t1.MajorSurgeryDetail,
                                 MasterEndDate = t1.MasterEndDate,
                                 MasterStartDate = t1.MasterStartDate,
                                 MentalPyhsicalProblemDetail = t1.MentalPhysicalProblemDetail,
                                 MiddleSchoolEndDate = t1.MiddleSchoolEndDate,
                                 MiddleSchoolStartDate = t1.MiddleSchoolStartDate,
                                 MilitaryInformation = t1.MilitaryInformation,
                                 MotherCurrentJob = t1.MotherCurrentJob,
                                 MotherName = t1.MotherName,
                                 MotherProfession = t1.MotherProfession,
                                 PhoneNumber = t1.PhoneNumber,
                                 PlaceOfBirth = t1.PlaceOfBirth,
                                 PossibleStartDate = t1.PossibleStartDate,
                                 PrimarySchoolEndDate = t1.PrimarySchoolEndDate,
                                 PrimarySchoolStartDate = t1.PrimarySchoolStartDate,
                                 Profession = t1.Profession,
                                 SpouseCurrentJob = t1.SpouseCurrentJob,
                                 SpouseName = t1.SpouseName,
                                 SpouseProfession = t1.SpouseProfession,
                                 SucscribedProfessionalOrganizations = t1.SucscribedProfessionalOrganizations,
                                 SucscribedSociety = t1.SucscribedSociety,
                                 SucscribedSporClubs = t1.SucscribedSporClubs,
                                 SucscribedUnions = t1.SucscribedUnions,
                                 UniversityEndDate = t1.UniversityEndDate,
                                 UniversityStartDate = t1.UniversityStartDate,
                                 IsDeleted = t1.IsDeleted,

                             };
                return filter == null ? result.FirstOrDefault() : result.Where(filter).FirstOrDefault();
            }
        }

    }
}
