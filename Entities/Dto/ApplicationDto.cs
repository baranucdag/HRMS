﻿using Entities.Abstract;
using System;

namespace Entities.Dto
{
    public class ApplicationDto : IDto
    {
        public int Id { get; set; }
        public int JobAdvertId { get; set; }
        public string PositionName { get; set; }
        public string QualificationLevel { get; set; }
        public string WorkTimeType { get; set; }
        public string WorkPlaceType { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime Deadline { get; set; }
        public int CandidateId { get; set; }
        public string CandidateFirstName { get; set; }
        public string CandidateLastName { get; set; }
        public string CandidateFullName => $"{CandidateFirstName} {CandidateLastName}";
        public DateTime ApplicationDate { get; set; }
        public string ApplicationStatus { get; set; }
        public string PrevApplicationStatus { get; set; }
        public bool HasEmailSent { get; set; }
        public DateTime CraetedAt { get; set; }
        public int IsDeleted { get; set; }
        public string IsDeletedText => (IsDeleted == 1) ? "yes" : "no";

    }
}
