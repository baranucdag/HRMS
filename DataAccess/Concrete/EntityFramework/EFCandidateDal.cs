using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using Entities.Dto;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCandidateDal : EfEntityRepositoryBase<Candidate, DataContext>, ICandidateDal
    {
        public List<CandidateDto> GetCandidateDetails()
        {
            List<CandidateDto> result = new List<CandidateDto>();
            using (var context = new DataContext())
            {
                var query = context.Set<Candidate>().Select(x => new CandidateDto
                {
                    Id = x.Id,
                    Adress = x.Adress,
                    ApplicationReasonDetail = x.ApplicationReasonDetail,
                    BloudGroup = x.BloudGroup,
                    CanTravel = x.CanTravel,
                    CommonAquaintances = x.CommonAquaintances,
                    CriminalRecordReason = x.CriminalRecordReason,
                    CvPath = x.CvPath,
                    DateOfBirth = x.DateOfBirth,
                    DeutchReadingLevel = x.DeutchReadingLevel,
                    DeutchSpeakingLevel = x.DeutchSpeakingLevel,
                    DeutchWritingLevel = x.DeutchWritingLevel,
                    DisabilityDetail = x.DisabilityDetail,
                    DrivingLicenceInformation = x.DrivingLicenceInformation,
                    EMail = x.EMail,
                    EnglishReadingLevel = x.EnglishReadingLevel,
                    EnglishSpeakingLevel = x.EnglishSpeakingLevel,
                    EnglishWritingLevel = x.EnglishWritingLevel,
                    ExpectedSalary = x.ExpectedSalary,
                    FatherCurrentJob = x.FatherCurrentJob,
                    FatherName = x.FatherName,
                    FatherProfession = x.FatherProfession,
                    FirstName = x.FirstName,
                    FrenchReadingLevel = x.FrenchReadingLevel,
                    FrenchSpeakingLevel = x.FrenchSpeakingLevel,
                    FrenchWritingLevel = x.FrenchWritingLevel,
                    Gender = x.Gender.ToString(),
                    HadMajorSurgery = x.HadMajorSurgery,
                    HasCriminalRecord = x.HasCriminalRecord,
                    HasDisability = x.HasDisability,
                    HasLawSuit = x.HasLawSuit,
                    HasMentalPhysicalProblem = x.HasMentalPhysicalProblem,
                    HasValidPassport = x.HasValidPassport,
                    HighSchoolEndDate = x.HighSchoolEndDate,
                    HighSchoolStartDate = x.HighSchoolStartDate,
                    HomePhoneNumber = x.HomePhoneNumber,
                    IsMarried = x.IsMarried,
                    JobForApplied = x.JobForApplied,
                    LastName = x.LastName,
                    LastPositionAdditionalBenefits = x.LastPositionAdditionalBenefits,
                    LastPositionDepartment = x.LastPositionDepartment,
                    LastPositionDescription = x.LastPositionDescription,
                    LastPositionManagerTitle = x.LastPositionManagerTitle,
                    LastPositionName = x.LastPositionName,
                    LawSuitReason = x.LawSuitReason,
                    MajorSurgeyDetail = x.MajorSurgeyDetail,
                    MasterEndDate = x.MasterEndDate,
                    MasterStartDate = x.MasterStartDate,
                    MentalPyhsicalProblemDetail = x.MentalPyhsicalProblemDetail,
                    MiddleSchoolEndDate = x.MiddleSchoolEndDate,
                    MiddleSchoolStartDate = x.MiddleSchoolStartDate,
                    MilitaryInformation = x.MilitaryInformation,
                    MotherCurrentJob = x.MotherCurrentJob,
                    MotherName = x.MotherName,
                    MotherProfession = x.MotherProfession,
                    PhoneNumber = x.PhoneNumber,
                    PlaceOfBirth = x.PlaceOfBirth,
                    PossibleStartDate = x.PossibleStartDate,
                    PrimarySchoolEndDate = x.PrimarySchoolEndDate,
                    PrimarySchoolStartDate = x.PrimarySchoolStartDate,
                    Profession = x.Profession,
                    SpouseCurrentJob = x.SpouseCurrentJob,
                    SpouseName = x.SpouseName,
                    SpouseProfession = x.SpouseProfession,
                    SucscribedProfessionalOrganizations = x.SucscribedProfessionalOrganizations,
                    SucscribedSociety = x.SucscribedSociety,
                    SucscribedSporClubs = x.SucscribedSporClubs,
                    SucscribedUnions = x.SucscribedUnions,
                    UniversityEndDate = x.UniversityEndDate,
                    UniversityStartDate = x.UniversityStartDate,
                    IsDeleted = x.IsDeleted,
                });
                result = query.ToList();
            }
            return result;
        }
    }
}
