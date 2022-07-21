﻿using Entities.Abstract;

namespace Entities.Concrete
{
    public class CandiateAnswer : IEntity
    {
        public int Id { get; set; }
        public int CanidateId { get; set; }
        public int AnswerId { get; set; }
        public int AnswerValue { get; set; }
    }
}
    