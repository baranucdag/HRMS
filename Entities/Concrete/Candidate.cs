using Entities.Abstract;
using System;

namespace Entities.Concrete
{
    public class Candidate : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Profession { get; set; }
        public bool IsMarried { get; set; }
        public string SpouseName { get; set; }
        public string SpouseProfession { get; set; }
        public string SpouseCurrentJob { get; set; }
        public string FatherName { get; set; }
        public string FatherProfession { get; set; }
        public string FatherCurrentJob { get; set; }
        public string MotherName { get; set; }
        public string MotherProfession { get; set; }
        public string MotherCurrentJob { get; set; }
        public bool HasCriminalRecord { get; set; }
        public string CriminalRecordReason { get; set; }
        public bool HasLawSuit { get; set; }
        public string LawSuitReason { get; set; }
        public bool HasDiability { get; set; }
        public string DisabilityDetail { get; set; }
        public bool HasMentalPhysicalProblem { get; set; }
        public string MentalPyhsicalProblemDetail { get; set; }
        public bool HadMajorSurgery { get; set; }
        public string MajorSurgeryDetail { get; set; }
        public string BloudGroup { get; set; }
        public string MilitaryInformation { get; set; }
        public string DrivingLicenceInformation { get; set; }
        public bool HasValidPassport { get; set; }
        public bool CanTravel { get; set; }
        public string JobForApplied { get; set; }
        public int ExpectedSalary { get; set; }
        public string ApplicationReasonDetail { get; set; }
        public string CommonAquaintances { get; set; }
        public DateTime PrimarySchoolStartDate { get; set; }
        public DateTime PrimarySchoolEndDate { get; set; }
        public DateTime MiddleSchoolStartDate { get; set; }
        public DateTime MiddleSchoolEndDate { get; set; }
        public DateTime HighSchoolStartDate { get; set; }
        public DateTime HightSchoolEndDate { get; set; }
        public DateTime UniversityStartDate { get; set; }
        public DateTime UniversityEndDate { get; set; }
        public DateTime MasterStartDate { get; set; }
        public DateTime MasterEndDate { get; set; }
        public string EnglishReadingLevel { get; set; }
        public string EnglishWritingLevel { get; set; }
        public string EnglishSpeakingLevel { get; set; }
        public string FrenchReadingLevel { get; set; }
        public string FrenchWritingLevel { get; set; }
        public string FrenchSpeakingLevel { get; set; }
        public string DeucthReadingLevel { get; set; }
        public string DeutchWritingLevel { get; set; }
        public string DeutchSpeaingLevel { get; set; }
        public string LastPositionName { get; set; }
        public string LastPositionDepartment { get; set; }
        public string LastPositionDescription { get; set; }
        public string LastPositionManagerTitle { get; set; }
        public string LastPositionAdditionalBenefits { get; set; }
        public DateTime PossibleStartDate { get; set; }
        public string Adress { get; set; }
        public string HomePhoneNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string SucscribedUnions { get; set; }
        public string SucscribedSociety { get; set; }
        public string SucscribedProfessionalOrganizations { get; set; }
        public string SucscribedSporClubs { get; set; }
        public bool IsDeleted { get; set; }
    }
}
